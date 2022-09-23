using Domain;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]

public class QuoteController:ControllerBase
{
 private QuoteServices _quoteService;
 public QuoteController(QuoteServices quoteService)
 {
    _quoteService = quoteService;
 }

 [HttpGet("GetQuotes")]
 public async Task<Response<List<Quotes>>> GetQuotess()
 {
   var result =  await _quoteService.GetQuotes();
   return result;
 }

 [HttpGet("GetByCategoryId")]
 public async Task<Response<List<Quotes>>> GetAllQuotesCategoryId(int Id)
 {
   var result = await _quoteService.GetQuotesByCategoryId(Id);
   return result;
 }

  [HttpGet("GetByCategoryDto")]
    public async Task<Response<List<GetQuoteByCategoryDto>>> GetQuoteByCategoryDto()
    {
        var result =  await _quoteService.GetQuoteByCategoryDto();
        return result;
    }


    [HttpGet("GetRandomQuotes")]
 public async Task<Response<List<Quotes>>> GetRandomQuotes()
 {
   var result = await _quoteService.GetRandomQuotes();
   return result;
 }
 

 [HttpPost("AddQuotes")]
public async Task<Response<Quotes>>  AddQuotes(Quotes quote)
{
   var result =  await _quoteService.AddQuotes(quote);
   return result;
}

[HttpPut("UpdateQuotes")]
public async Task<Response<Quotes>> UpdateQuotes(Quotes quote)
{
   var res = await _quoteService.UpdateQuotess(quote);
   return res;
}

[HttpDelete("DeleteQuotes")]
public async Task<Response<int>> DeleteQuotes(int id)
{
 return await _quoteService.DeleteQuotess(id);
}

}
