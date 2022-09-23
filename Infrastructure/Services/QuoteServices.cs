namespace Infrastructure.Services;
using Dapper;
using Domain;
using Domain.Dtos;
using Domain.Wrapper;
using Npgsql;
using Infrastructure.DataContext;
public class QuoteServices
{

    private DataContext _context;
    public QuoteServices(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Quotes>>> GetQuotes()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
             var response = await connection.QueryAsync<Quotes>($"select * from Quotes;");
            return new Response<List<Quotes>>(response.ToList());   
            }
            catch (Exception ex)
            {
                return new Response<List<Quotes>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }

    public async Task<Response<List<Quotes>>> GetRandomQuotes()
    {
        using (var connection = _context.CreateConnection())
        {
            try
        {
            
                var response = await connection.QueryAsync<Quotes>($"select * from Quotes ORDER BY random() limit 1;");
                return new Response<List<Quotes>>(response.ToList());
        }
        catch (Exception ex)
            {
                return new Response<List<Quotes>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
    public async Task<Response<List<GetQuoteByCategoryDto>>> GetQuoteByCategoryDto()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                
                var response = await connection.QueryAsync<GetQuoteByCategoryDto>($"SELECT q.id,q.quotetext,q.author,c.CategoryName FROM Categories as c INNER JOIN quotes as q ON q.categoryid = c.id; ");
                return new Response<List<GetQuoteByCategoryDto>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<GetQuoteByCategoryDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<List<Quotes>>> GetQuotesByCategoryId(int Id)
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {    
                var response = await connection.QueryAsync<Quotes>($"select * from Quotes where CategoryId = '{Id}';");
                return new Response<List<Quotes>>(response.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<Quotes>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<Quotes>> AddQuotes(Quotes quote)
    {
        // Add Quotess to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Quotes (QuoteText,Author,CategoryId) values ('{quote.QuoteText}', '{quote.Author}',{quote.CategoryId}) returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                quote.Id = id;
                return new Response<Quotes>(quote);
            }
            catch (Exception ex)
            {
                return new Response<Quotes>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
    public async Task<Response<int>> DeleteQuotess(int id)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                connection.Open();
                string sql = $"delete from Quotes where id = '{id}';";
                var response = await connection.ExecuteAsync(sql);
                return new Response<int>(response);
            }
            catch (System.Exception ex)
            {

                return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<Quotes>> UpdateQuotess(Quotes quote)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
             string sql = $"UPDATE Quotes SET QuoteText='{quote.QuoteText}',Author='{quote.Author}',CategoryId = '{quote.CategoryId}' WHERE id = {quote.Id} returning id;";
            var id = await connection.ExecuteScalarAsync<int>(sql);
            quote.Id = id;
            return new Response<Quotes>(quote);   
            }
            catch (Exception ex)
            {
                return new Response<Quotes>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
            
        }
    }


}

