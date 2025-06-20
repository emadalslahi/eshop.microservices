using Eshop.Discount.Grpc.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Discount.Grpc.Services;

public class DiscountService : Grpc.Protos.DiscountService.DiscountServiceBase
{
    private readonly ILogger<DiscountService> _logger;
    private readonly DiscountDbContext _context;
    public DiscountService(ILogger<DiscountService> logger , DiscountDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context; // ?? throw new ArgumentNullException(nameof(discountRepository));
    }
    //public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    //{
    //    var coupon = await _discountRepository.GetDiscount(request.ProductName);
    //    if (coupon == null)
    //    {
    //        return new CouponModel();
    //    }
    //    return new CouponModel
    //    {
    //        Id = coupon.Id,
    //        ProductName = coupon.ProductName,
    //        Description = coupon.Description,
    //        Amount = coupon.Amount
    //    };
    //}
    public override async Task<Protos.CreateDiscountResponse> CreateDiscount(Protos.CreateDiscountRequest request, ServerCallContext context)
    {
        var copn = new Models.Coupon
        {
            ProductName = request.ProductName,
            Description = request.Description,
            Amount = request.Amount
        };
        _logger.LogInformation("Creating discount for ProductName: {ProductName}, Amount: {Amount}", request.ProductName, request.Amount);
        if (string.IsNullOrEmpty(copn.ProductName) || copn.Amount < 0)
        {
            _logger.LogError("Invalid Request Missing Product Name or Amount is Less Than 0");
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Missing Product Name or Amount is Less Than 0"));
        }
        await _context.AddAsync(copn, context.CancellationToken);
        await _context.SaveChangesAsync(context.CancellationToken);
        _logger.LogInformation("Discount created with Id: {Id}", copn.Id);
        return new Protos.CreateDiscountResponse
        {
            Id = copn.Id,
            ProductName = copn.ProductName,
            Description = copn.Description,
            Amount = (int)copn.Amount
        };
    }
    public override async Task<Protos.DeleteDiscountResponse> DeleteDiscount(Protos.DeleteDiscountRequest request, ServerCallContext context)
    {
        _logger.LogInformation("DeleteDiscount called with ProductName: {ProductName}", request.ProductName);
        var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon == null)
        {
            _logger.LogWarning("No discount found for ProductName: {ProductName}", request.ProductName);
            return new Protos.DeleteDiscountResponse { Success = false };
        }
        _context.Coupons.Remove(coupon);
        _logger.LogInformation("Deleting discount for ProductName: {ProductName}, Id: {Id}", coupon.ProductName, coupon.Id);
        await _context.SaveChangesAsync(context.CancellationToken);
        return new Protos.DeleteDiscountResponse
        {
            Success = true,
            ProductName = coupon.ProductName
        };
    }
    override public Task<Protos.UpdateDiscountResponse> UpdateDiscount(Protos.UpdateDiscountRequest request, ServerCallContext context)
    {

        _logger.LogInformation("UpdateDiscount called with ProductName: {ProductName}", request.ProductName);
        var coupon = _context.Coupons.FirstOrDefault(x => x.ProductName == request.ProductName);
        if (coupon == null)
        {
            _logger.LogWarning("No discount found for ProductName: {ProductName}", request.ProductName);
            return Task.FromResult(new Protos.UpdateDiscountResponse { Success = false });
        }
        coupon.Description = request.Description;
        coupon.Amount = request.Amount;
        _context.Coupons.Update(coupon);
        _logger.LogInformation("Updating discount for ProductName: {ProductName}, Id: {Id}", coupon.ProductName, coupon.Id);
        _context.SaveChanges();
        return Task.FromResult(new Protos.UpdateDiscountResponse
        {
            Success = true,
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = (int)coupon.Amount
        });
    }
    public override async Task<Protos.GetDiscountResponse> GetDiscount(Protos.GetDiscountRequest request, ServerCallContext context)
    {
        _logger.LogInformation("GetDiscount called with ProductName: {ProductName}", request.ProductName);
        var coupon = await   _context.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
        if (coupon == null)
        {
            _logger.LogWarning("No discount found for ProductName: {ProductName}", request.ProductName);
            return new Protos.GetDiscountResponse();
        }
        _logger.LogInformation("Found discount for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
        return new Protos.GetDiscountResponse
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = (int)coupon.Amount
        };
    }
}
