namespace RollsApi.Controllers
{
  public class TokenCheck : Attribute, IAuthorizationFilter
  {
    private readonly IConfiguration _configuration;
    private readonly string _key;

    public TokenCheck(IConfiguration configuration)
    {

      _configuration = configuration;
      _key = _configuration.GetValue<String>("ApiKeys:Rolls");
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var Apikey = (string)(context.HttpContext.Request.Headers["Api-Key"]);

      if (Apikey != _key)
      {
        Log.Error("Roles: Missing/Invalid Api Key", "Roles: Missing/Invalid Api Key");
        context.Result = new StatusCodeResult(401);

      }
    }
  }
}
