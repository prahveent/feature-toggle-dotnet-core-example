using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace FeatureToggle.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class FeatureToggleController : ControllerBase
	{
		private readonly IFeatureManager _featureManager;

		public FeatureToggleController(IFeatureManager featureManager)
		{
			_featureManager = featureManager;
		}

		[HttpGet("price")]
		public async Task<IActionResult> GetPrice()
		{
			decimal price = 100;
			if (await _featureManager.IsEnabledAsync(Features.GetPrice))
			{
				// half done feature
				price = GetPriceBasedOnMetrix();
			}
			return this.Ok(price);
		}

		[HttpGet("count")]
		public async Task<IActionResult> GetCount()
		{
			int count = 10;
			if (await _featureManager.IsEnabledAsync(Features.GetCount))
			{
				count = GetCountWithSomeLogic();
			}
			return this.Ok(count);
		}


		private decimal GetPriceBasedOnMetrix()
		{
			return (decimal) 500.00;
		}

		private int GetCountWithSomeLogic()
		{
			return 40 * 10;
		}

	}
}

