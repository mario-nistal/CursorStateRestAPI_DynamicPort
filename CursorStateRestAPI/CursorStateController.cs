using System.Web.Http;
using GetCursorState;

public class CursorStateController : ApiController
{
    [HttpGet]
    public IHttpActionResult Get()
    {
        string cursorState = GetCursorStateMethods.CheckCursorTypeString();
        var response = new { cursorState = cursorState };
        return Ok(response);
    }
}