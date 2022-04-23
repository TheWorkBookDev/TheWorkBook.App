﻿using System.Collections.Specialized;
using TheWorkBook.Dto;
using TheWorkBook.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Linq;

namespace TheWorkBook.Data
{
    public class UserService
    {
        private const string _version = "v1";
        readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> GetMyInfo()
        {
            UserDto user = await _httpClient.MakeGetRequest<UserDto>($"/{_version}/user/getMyInfo");

            // Authenticated request!
            return user;
        }

        public async Task<string> UpdateMyInfo(UserDto userDto)
        {
            UserDto myInfo = await GetMyInfo();
            JsonPatchDocument body = JsonPatchDocumentDiff.CalculatePatch(myInfo, userDto);
            
            string path = $"/{_version}/user/updateMyInfo";

            string result = await _httpClient.MakePatchRequest<string, JsonPatchDocument>(path, body);
            return result;
        }
    }
}
