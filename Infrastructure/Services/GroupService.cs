namespace Infrastructure.Services;
using Dapper;
using Domain;
using Domain.Wrapper;
using Npgsql;
using Infrastructure.DataContext;
using Domain.Entities;

public class GroupService
{

    private DataContext _context;
    public GroupService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Groupes>>> GetGroups()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
             var response = await connection.QueryAsync<Groupes>($"select * from Groupes;");
            return new Response<List<Groupes>>(response.ToList());   
            }
            catch (Exception ex)
            {
                return new Response<List<Group>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
    

    public async Task<Response<Group>> AddGroup(Group Group)
    {
        // Add Groupss to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                string sql = $"insert into Group (FirstName,LastName,Email,Phone,Address,City) values ('{Group.FirstName}', '{Group.LastName}','{Group.Email}','{Group.Phone}','{Group.Adress}','{Group.City}') returning id";
                var id = await connection.ExecuteScalarAsync<int>(sql);
                Group.Id = id;
                return new Response<Group>(Group);
            }
            catch (Exception ex)
            {
                return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }
    }
    public async Task<Response<int>> DeleteGroup(int id)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
                connection.Open();
                string sql = $"delete from Group where id = '{id}';";
                var response = await connection.ExecuteAsync(sql);
                return new Response<int>(response);
            }
            catch (System.Exception ex)
            {

                return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public async Task<Response<Group>> UpdateGroupss(Group Group)
    {
        // Add contact to database
        using (var connection = _context.CreateConnection())
        {
            try
            {
             string sql = $"UPDATE Group SET FirstName='{Group.FirstName}',LastName='{Group.LastName}',Email = '{Group.Email}',Phone='{Group.Phone}',Address='{Group.Adress}',City='{Group.City}' WHERE id = {Group.Id} returning id;";
            var id = await connection.ExecuteScalarAsync<int>(sql);
            Group.Id = id;
            return new Response<Group>(Group);   
            }
            catch (Exception ex)
            {
                return new Response<Group>(System.Net.HttpStatusCode.InternalServerError,ex.Message);
            }
            
        }
    }

}

