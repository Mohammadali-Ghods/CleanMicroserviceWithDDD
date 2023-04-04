using Application.Base.ViewModels;
using Application.Interface;
using ExternalApi.ConfigurationModel;
using ExternalApi.HttpModule;
using ExternalApi.TokenService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApi.Api
{
    public class UserAPI : IUserAPI
    {
        #region Fields
        private readonly ExretnalApiModel _exretnalApiModel;
        private readonly ISwtToken _swtToken;
        #endregion

        #region Ctor
        public UserAPI(IOptionsMonitor<ExretnalApiModel> optionsMonitor,
            ISwtToken swtToken)
        {
            _exretnalApiModel = optionsMonitor.CurrentValue;
            _swtToken = swtToken;
        }
        #endregion

        public async Task<BriefUserViewModel> GetUser(string userid)
        {
            var swttoken = "Bearer " + _swtToken.JwtGenerator();
            return await BaseHttp.Get<BriefUserViewModel>(new Dictionary<string, string>(){
                    {"Authorization",swttoken }}
                , _exretnalApiModel.CustomerMicroserviceUrl + "/" + userid, null);
        }
    }
}
