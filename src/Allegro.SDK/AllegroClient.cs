﻿using Allegro.SDK.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Allegro.SDK
{
    public class AllegroClient
    {
        public HttpClient _client { get; }
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;
        private readonly EnvEnum _envEnum;

        public AllegroClient(HttpClient client, string clientId, string clientSecret, EnvEnum envEnum = EnvEnum.Dev)
        {
            _client = client;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        private string GetApiBaseUrl()
        {
            switch (_envEnum)
            {
                case EnvEnum.Dev:
                    return "https://api.allegro.pl.allegrosandbox.pl/";
                case EnvEnum.Prod:
                    return "https://api.allegro.pl/";
                default:
                    return "https://api.allegro.pl.allegrosandbox.pl/";
            }
        }
        private class JsonContent : StringContent
        {
            public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }
        /// <summary>
        /// Get方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AllegroResult<T>> GetAsync<T,K>(BaseRequest<T,K> request)
        {

            var url = "";

            AllegroResult<T> result = new AllegroResult<T>();

            _client.DefaultRequestHeaders.Clear();

            _client.DefaultRequestHeaders.Add("Accept", "application/vnd.allegro.public.v1+json");

            _client.DefaultRequestHeaders.Add("Accept-Language", "en-US");

            if (request.Request == RequestEnum.Refresh)
            {

                url = "https://allegro.pl.allegrosandbox.pl/auth/oauth" + request.Url + "&redirect_uri=" + _redirectUri;

                byte[] bytes = Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret);

                _client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(bytes));

            }

            if (request.Request == RequestEnum.User)
            {

                url = "https://allegro.pl.allegrosandbox.pl/auth/oauth" + request.Url+ "&redirect_uri="+ _redirectUri;

                byte[] bytes = Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret);

                _client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(bytes));

            }

            if (request.Request==RequestEnum.App)
            {
                url = "https://allegro.pl.allegrosandbox.pl/auth/oauth" + request.Url;

                byte[] bytes = Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret);

                _client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(bytes));

            }

            if (request.Request == RequestEnum.Api)
            {
                url = $"{GetApiBaseUrl()}{request.Url}";

                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);

            }

            var httpResponse = await _client.GetAsync(url);

            var content = await httpResponse.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(content);

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                result.Failed(httpResponse.StatusCode.ToString());
            }
            else
            {
                result.Success(httpResponse.StatusCode.ToString());
            }
            result.Result = obj;

            return await Task.FromResult(result);
        }
        /// <summary>
        /// Post方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AllegroResult<T>> PostAsync<T,K>(BaseRequest<T,K> request)
        {
            AllegroResult<T> result = new AllegroResult<T>();

            var url = $"{GetApiBaseUrl()}";

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);

            var httpResponse = await _client.PostAsync(url, new JsonContent(new { request.Data }));

            var content = await httpResponse.Content.ReadAsStringAsync();

            T obj = JsonConvert.DeserializeObject<T>(content);

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                result.Failed(httpResponse.StatusCode.ToString());
            }
            else
            {
                result.Success(httpResponse.StatusCode.ToString());
            }
            result.Result = obj;

            return await Task.FromResult(result);
        }
    }
}
