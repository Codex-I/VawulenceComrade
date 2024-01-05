using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace VawulenceComrade.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public IActionResult OnGetRefresh()
        {
            return RedirectToPage("Index");
        }


        [BindProperty(SupportsGet = true)]
        public insultInnerObj? deserialInsult { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                var client = new HttpClient();
                var insultUrl = "https://evilinsult.com/generate_insult.php?lang=en&type=json";
                var insultResponse = await client.GetAsync(insultUrl);
                insultResponse.EnsureSuccessStatusCode();
                var responseContent = await insultResponse.Content.ReadAsStringAsync();
                deserialInsult = JsonConvert.DeserializeObject<insultInnerObj>(responseContent);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
        }
    }
}


//public class insultResponseObj
//{
//    public insultInnerObj slip { get; set; }
//}

public class insultInnerObj
{
    public int number { get; set; }
    public string insult { get; set; }
}