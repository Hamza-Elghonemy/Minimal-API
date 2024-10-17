using AutoMapper;
using FluentValidation;
using MagicVanilla_CouponAPI;
using MagicVanilla_CouponAPI.Data;
using MagicVanilla_CouponAPI.Models;
using MagicVanilla_CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
//builder.Services.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// GET all coupons
app.MapGet("/api/coupon", (ILogger<Program> _logger) => {
    API_Response response = new ();
    _logger.Log(LogLevel.Information, "Getting All Coupons");
    response.Result = CouponStore.couponList;
    response.IsSuccess = true;
    response.HttpStatusCode = HttpStatusCode.OK;
    return Results.Ok(response);
    
 }).WithName("GetCoupons").Produces<API_Response>(200);

// GET coupon by id
app.MapGet("/api/coupon/{id:int}", (ILogger < Program > _logger, int id) => {
    API_Response response = new();
    response.Result = CouponStore.couponList.FirstOrDefault(u => u.Id == id);
    response.IsSuccess = true;
    response.HttpStatusCode = HttpStatusCode.OK;
    return Results.Ok(response);

}).WithName("GetCoupon").Produces<API_Response>(200); // setting the response type to Coupon


// POST a new coupon
app.MapPost("api/coupon_2", async (IValidator<CouponCreateDTO> _validation, IMapper _mapper,[FromBody] CouponCreateDTO coupon_C_DTO) =>
{

    API_Response response = new() { IsSuccess = false, HttpStatusCode = HttpStatusCode.BadRequest };

    var validationResult = await _validation.ValidateAsync(coupon_C_DTO);
    if (!validationResult.IsValid)
    {
        response.ErrorMessage.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    if (CouponStore.couponList.FirstOrDefault(u => u.Name.Equals(coupon_C_DTO.Name, StringComparison.CurrentCultureIgnoreCase)) != null)
    {
        response.ErrorMessage.Add("Coupon already exists");
        return Results.BadRequest(response);
    }
    Coupon coupon = _mapper.Map<Coupon>(coupon_C_DTO);
    coupon.Id = CouponStore.couponList.Max(u => u.Id) + 1;
    CouponStore.couponList.Add(coupon);

    CouponDTO couponDTO =_mapper.Map<CouponDTO>(coupon);
   
    response.Result = couponDTO;
    response.IsSuccess = true;
    response.HttpStatusCode = HttpStatusCode.OK;
    return Results.Ok(response);

    //Results.CreatedAtRoute("GetCoupon", new { id = coupon.Id }, couponDTO);
    //return Results.Created($"/api/coupon_2/{coupon.Id}",coupon);
}).WithName("CreateCoupon").Accepts<CouponCreateDTO>("application/json").Produces<API_Response>(201).Produces(400);


// PUT update a coupon
app.MapPut("/api/coupon", async (IValidator<CouponUpdateDTO> _validation, IMapper _mapper, [FromBody] CouponUpdateDTO coupon_U_DTO) =>
{

    API_Response response = new() { IsSuccess = false, HttpStatusCode = HttpStatusCode.BadRequest };

    var validationResult = await _validation.ValidateAsync(coupon_U_DTO);
    if (!validationResult.IsValid)
    {
        response.ErrorMessage.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    var existingCoupon = CouponStore.couponList.FirstOrDefault(u => u.Id == coupon_U_DTO.Id);
    if (existingCoupon == null)
    {
        response.ErrorMessage.Add("Coupon does not exist");
        return Results.NotFound(response);
    }
    existingCoupon.Name = coupon_U_DTO.Name;
    existingCoupon.PercentOff = coupon_U_DTO.PercentOff;
    existingCoupon.IsActive = coupon_U_DTO.IsActive;
    existingCoupon.LastUpdated = DateTime.Now;

    CouponDTO couponDTO = _mapper.Map<CouponDTO>(existingCoupon);

    response.Result = couponDTO;
    response.IsSuccess = true;
    response.HttpStatusCode = HttpStatusCode.OK;
    return Results.Ok(response);

}).WithName("UpdateCoupon").Accepts<CouponUpdateDTO>("application/json").Produces<API_Response>(200).Produces(400).Produces(404);


// DELETE a coupon
app.MapDelete("/api/coupon/{id:int}",(int id ) =>
{

    API_Response response = new() { IsSuccess = false, HttpStatusCode = HttpStatusCode.BadRequest };

    var existingCoupon = CouponStore.couponList.FirstOrDefault(u => u.Id == id);
    if (existingCoupon != null)
    {
        var existingCoupon_id = existingCoupon.Id;
        CouponStore.couponList.Remove(existingCoupon);
        response.IsSuccess = true;
        response.HttpStatusCode = HttpStatusCode.NoContent;
        response.Message = $"Deleted coupon with id: {existingCoupon_id}";
        return Results.Ok(response);
    }
    else
    {
        response.ErrorMessage.Add("Invaild ID, coupon does not exist");
        return Results.BadRequest(response);
    }
    
});


app.UseHttpsRedirection();
app.Run();


 