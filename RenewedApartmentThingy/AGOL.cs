using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace RenewedApartmentThingy
{
    public class AGOL
    {
        private string _token;
        private string _username;
        private string _password;
        public organizationInformation orgInfo;


        public string Token
        {
            get
            {
                return _token;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public AGOL(string UserName, string PassWord)
        {
            _username = UserName;
            _password = PassWord;
            _token = GetToken(UserName, PassWord);
            orgInfo = _getOrgInfo(_token);
        }



        public string GetToken(string username, string password)
        {

            var data = new NameValueCollection();
            data["username"] = Username;
            data["password"] = Password;
            data["referer"] = "https://www.arcgis.com";
            data["f"] = "json";

            TokenInfo x = JsonConvert.DeserializeObject<TokenInfo>(_getResponse(data, "https://arcgis.com/sharing/rest/generateToken"));
            return x.token; ;
        }

        private organizationInformation _getOrgInfo(string token)
        {
            var data = new NameValueCollection();
            data["token"] = token;
            data["f"] = "json";

            organizationInformation x = JsonConvert.DeserializeObject<organizationInformation>(_getResponse(data, "http://www.arcgis.com/sharing/rest/portals/self"));
            return x;
        }

        private string _getResponse(NameValueCollection data, string url)
        {
            string responseData;
            var webClient = new System.Net.WebClient();
            var response = webClient.UploadValues(url, data);
            responseData = System.Text.Encoding.UTF8.GetString(response);
            return responseData;
        }

        //Starts the collection of useless classes....
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        public class TokenInfo
        {
            public string token { get; set; }
            public long expires { get; set; }
            public bool ssl { get; set; }
        }

        public class organizationInformation
        {
            public string access { get; set; }
            public bool allSSL { get; set; }
            public double availableCredits { get; set; }
            public string backgroundImage { get; set; }
            public string basemapGalleryGroupQuery { get; set; }
            public string bingKey { get; set; }
            public bool canListApps { get; set; }
            public bool canListData { get; set; }
            public bool canListPreProvisionedItems { get; set; }
            public bool canProvisionDirectPurchase { get; set; }
            public bool canSearchPublic { get; set; }
            public bool canShareBingPublic { get; set; }
            public bool canSharePublic { get; set; }
            public bool canSignInArcGIS { get; set; }
            public bool canSignInIDP { get; set; }
            public string colorSetsGroupQuery { get; set; }
            public bool commentsEnabled { get; set; }
            public long created { get; set; }
            public string culture { get; set; }
            public string customBaseUrl { get; set; }
            public int databaseQuota { get; set; }
            public int databaseUsage { get; set; }
            public string description { get; set; }
            public string featuredGroupsId { get; set; }
            public string featuredItemsGroupQuery { get; set; }
            public string galleryTemplatesGroupQuery { get; set; }
            public string helpBase { get; set; }
            public string homePageFeaturedContent { get; set; }
            public int homePageFeaturedContentCount { get; set; }
            public int httpPort { get; set; }
            public int httpsPort { get; set; }
            public string id { get; set; }
            public string ipCntryCode { get; set; }
            public bool isPortal { get; set; }
            public string layerTemplatesGroupQuery { get; set; }
            public int maxTokenExpirationMinutes { get; set; }
            public long modified { get; set; }
            public string name { get; set; }
            public string portalHostname { get; set; }
            public string portalMode { get; set; }
            public string portalName { get; set; }
            public object portalThumbnail { get; set; }
            public string region { get; set; }
            public bool showHomePageDescription { get; set; }
            public string staticImagesUrl { get; set; }
            public long storageQuota { get; set; }
            public long storageUsage { get; set; }
            public bool supportsHostedServices { get; set; }
            public bool supportsOAuth { get; set; }
            public string symbolSetsGroupQuery { get; set; }
            public string templatesGroupQuery { get; set; }
            public string thumbnail { get; set; }
            public string units { get; set; }
            public string urlKey { get; set; }
            public bool useStandardizedQuery { get; set; }
        }
    }
}