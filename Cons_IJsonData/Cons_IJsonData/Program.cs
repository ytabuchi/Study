using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.Collections.ObjectModel;

namespace Cons_IJsonData
{
    class Program
    {
        //https://raw.githubusercontent.com/ytabuchi/Study/master/Cons_IJsonData/Cons_IJsonData/ConnpassJson.json
        public const string connpassJson = @"
{""results_returned"": 10, ""events"": [{""event_url"": ""http://osaka-prml-reading.connpass.com/event/39753/"", ""event_type"": ""participation"", ""owner_nickname"": ""wrist"", ""series"": {""url"": ""http://osaka-prml-reading.connpass.com/"", ""id"": 2614, ""title"": ""\u5927\u962aPRML\u8aad\u66f8\u4f1a""}, ""updated_at"": ""2016-09-06T12:46:35+09:00"", ""lat"": null, ""started_at"": ""2016-09-11T21:00:00+09:00"", ""hash_tag"": """", ""title"": ""IPython\u30c7\u30fc\u30bf\u30b5\u30a4\u30a8\u30f3\u30b9\u30af\u30c3\u30af\u30d6\u30c3\u30af \u30aa\u30f3\u30e9\u30a4\u30f3\u8aad\u66f8\u4f1a#22"", ""event_id"": 39753, ""lon"": null, ""waiting"": 0, ""limit"": 25, ""owner_id"": 6544, ""owner_display_name"": ""wrist"", ""description"": ""<h2>IPython\u30c7\u30fc\u30bf\u30b5\u30a4\u30a8\u30f3\u30b9\u30af\u30c3\u30af\u30d6\u30c3\u30af \u30aa\u30f3\u30e9\u30a4\u30f3\u8aad\u66f8\u4f1a#22</h2>\n<p><a href=\""https://www.oreilly.co.jp/books/9784873117485/\"" rel=\""nofollow\"">IPython\u30c7\u30fc\u30bf\u30b5\u30a4\u30a8\u30f3\u30b9\u30af\u30c3\u30af\u30d6\u30c3\u30af</a>\u306e\u30aa\u30f3\u30e9\u30a4\u30f3\u8aad\u66f8\u4f1a\u7b2c22\u56de\u76ee\u30929/11(\u65e5)\u306e21:00-22:30\u306b\u958b\u50ac\u3057\u307e\u3059\u3002</p>\n<p>\u30aa\u30f3\u30e9\u30a4\u30f3\u8aad\u66f8\u4f1a\u3068\u3044\u3046\u3053\u3068\u3067skype\u3092\u4f7f\u3063\u3066\u4f1a\u8a71\u3057\u3064\u3064\u672c\u3092\u8aad\u307f\u3001\u968f\u6642slack\u3092\u4f7f\u3063\u3066\u30b3\u30fc\u30c9\u3092\u5171\u6709\u3057\u307e\u3059\u3002\n<a href=\""https://join.skype.com/HTeGimXaBS1q\"" rel=\""nofollow\"">skype\u30b0\u30eb\u30fc\u30d7\u306eURL</a>\u306b\u30a2\u30af\u30bb\u30b9\u3059\u308b\u3068\u4f1a\u8a71\u306b\u53c2\u52a0\u3059\u308b\u3053\u3068\u304c\u3067\u304d\u307e\u3059\u306e\u3067\u3001\u53c2\u52a0\u3059\u308b\u65b9\u306f\u3053\u306e\u30a4\u30d9\u30f3\u30c8\u306b\u767b\u9332\u306e\u4e0a\u3001\u6642\u9593\u307e\u3067\u306b\u53c2\u52a0\u3057\u3066\u304a\u3044\u3066\u3044\u305f\u3060\u3051\u308c\u3070\u3068\u601d\u3044\u307e\u3059\u3002\n\u307e\u305f\u3001\u521d\u3081\u3066\u53c2\u52a0\u3055\u308c\u308b\u65b9\u306f\u53c2\u52a0\u767b\u9332\u6642\u306e\u30a2\u30f3\u30b1\u30fc\u30c8\u306bslack\u767b\u9332\u7528\u306eemail\u30a2\u30c9\u30ec\u30b9\u3092\u66f8\u3044\u3066\u3044\u305f\u3060\u3051\u308c\u3070\u30b0\u30eb\u30fc\u30d7\u306b\u62db\u5f85\u3044\u305f\u3057\u307e\u3059\u3002\n\u306a\u304a\u6bce\u56de22:30-23:00\u306f\u6b21\u56de\u65e5\u7a0b\u6c7a\u3081\u3084\u96d1\u8ac7\u306a\u3069\u3092\u884c\u3063\u3066\u304a\u308a\u307e\u3059\u306e\u3067\u3001\u57fa\u672c\u7684\u306b\u52c9\u5f37\u4f1a\u3068\u3057\u3066\u306f22:30\u307e\u3067\u3067\u3059\u3002</p>\n<h2>\u5185\u5bb9</h2>\n<p>\u524d\u56de\u306f15\u7ae0(Sympy\u306e\u7ae0)\u306e15.2\u307e\u3067\u3092\u8aad\u307f\u307e\u3057\u305f\u306e\u3067\u3001\u4eca\u56de\u306f15\u7ae0\u306e\u6b8b\u308a\u306e\u30ec\u30b7\u30d4\u3092\u8aad\u3093\u3067\u3044\u304d\u307e\u3059\u3002\u5404\u30ec\u30b7\u30d4\u3092\u6642\u9593\u5185\u3067\u8aad\u3081\u308b\u3060\u3051\u8aad\u3080\u3068\u3044\u3046\u5f62\u5f0f\u3067\u884c\u3063\u3066\u304a\u308a\u307e\u3059\u3002</p>\n<h3>15\u7ae0\u3000\u8a18\u53f7\u51e6\u7406\u3068\u6570\u5024\u89e3\u6790</h3>\n<ul>\n<li>\u30ec\u30b7\u30d4 15.3\u5b9f\u6570\u5024\u95a2\u6570\u306e\u89e3\u6790</li>\n<li>\u30ec\u30b7\u30d4 15.4\u6b63\u78ba\u78ba\u7387\u306e\u8a08\u7b97\u3068\u78ba\u7387\u5909\u6570\u306e\u64cd\u4f5c</li>\n<li>\u30ec\u30b7\u30d4 15.5 SymPy\u3092\u4f7f\u3063\u305f\u7c21\u5358\u306a\u6570\u8ad6</li>\n<li>\u30ec\u30b7\u30d4 15.6\u771f\u7406\u5024\u8868\u304b\u3089\u8ad6\u7406\u547d\u984c\u5f0f\u3092\u751f\u6210</li>\n<li>\u30ec\u30b7\u30d4 15.7\u975e\u7dda\u5f62\u5fae\u5206\u7cfb\u306e\u5206\u6790\uff1a\u30ed\u30c8\u30ab\u30fb\u30f4\u30a9\u30eb\u30c6\u30e9\uff08\u6355\u98df\u8005\u3068\u88ab\u98df\u8005\uff09\u65b9\u7a0b\u5f0f</li>\n<li>\u30ec\u30b7\u30d4 15.8\u306f\u3058\u3081\u3066\u306eSage</li>\n</ul>\n<h2>\u8af8\u6ce8\u610f</h2>\n<ul>\n<li>jupyter notebook\u306e\u74b0\u5883\u69cb\u7bc9\u306f\u3067\u304d\u308b\u304b\u304e\u308a\u4e88\u3081\u5404\u81ea\u3067\u6e08\u307e\u305b\u3066\u304f\u3060\u3055\u3044(anaconda\u3092\u4f7f\u3046\u306e\u304c\u30aa\u30b9\u30b9\u30e1\u3067\u3059)\u3002\u3067\u304d\u3066\u306a\u3044\u5834\u5408\u3067\u3082\u53c2\u52a0\u3044\u305f\u3060\u3051\u308c\u3070\u30b5\u30dd\u30fc\u30c8\u3067\u304d\u308b\u304b\u3082\u3057\u308c\u307e\u305b\u3093\u3002</li>\n<li>\u672c\u306b\u3082\u8a18\u8f09\u304c\u3042\u308a\u307e\u3059\u304c<a href=\""https://github.com/ipython-books\"" rel=\""nofollow\"">\u3053\u3053</a>\u304b\u3089\u5168\u30ec\u30b7\u30d4\u306e\u30b3\u30fc\u30c9\u3068\u53c2\u8003\u8cc7\u6599\u3078\u306e\u30ea\u30f3\u30af\u304c\u53d6\u5f97\u3067\u304d\u307e\u3059\u306e\u3067\u4e88\u3081clone\u3057\u3066\u304a\u304f\u3068\u826f\u3044\u304b\u3068\u601d\u3044\u307e\u3059\u3002</li>\n</ul>\n<p>\u3054\u8208\u5473\u306e\u3042\u308b\u65b9\u3001\u305c\u3072\u3054\u53c2\u52a0\u3092\u304a\u9858\u3044\u3057\u307e\u3059\u3002</p>"", ""address"": """", ""catch"": """", ""accepted"": 3, ""ended_at"": ""2016-09-11T23:00:00+09:00"", ""place"": ""Skype & Slack""}],results_start: 1,results_available: 1003}
";
        //https://raw.githubusercontent.com/ytabuchi/Study/master/Cons_IJsonData/Cons_IJsonData/AtndJson.json
        string atndJson = @"
{""results_returned"":1,""results_start"":1,""events"":[{""event"":{""event_id"":60918,""title"":""アニメ語りたいin梅田　1/25（日）"",""catch"":""梅田1/25(日)"",""description"":""\u003cp\u003eはじめに\u003cbr /\u003e\n登録の手順\u003c/p\u003e\n\u003cp\u003e1.Google、Yahoo!、mixiなどのIDを利用しログインします。\u003cbr /\u003e\n2.名前を登録します。\u003cbr /\u003e\n3.設定にてメールアドレスを登録します。(メッセのやりとりがあると通知されます）\u003c/p\u003e\n\u003cp\u003e内容はこちら。 \u003cbr /\u003e\nお酒を飲みますので未成年の参加はダメよー、ダメダメ。\u003c/p\u003e\n\u003cp\u003e【日時（予定）】1/25（日）19:00スタート（2時間） \u003cbr /\u003e\n【場所（予定）】個室空間　楽宴の贈りもの　梅田店\u003cbr /\u003e\n【会費（予定）】男性3500円\u003cbr /\u003e\n　　　　　　　　女性2500円\u003cbr /\u003e\n【人数】20名\u003cbr /\u003e\n【募集期限】1/18（日）\u003c/p\u003e\n\u003cp\u003e参加してくださる人は\u003c/p\u003e\n\u003cp\u003e【名前】 \u003cbr /\u003e\n【性別】 \u003cbr /\u003e\n【成人ですか？】 \u003cbr /\u003e\n【好きな作品】 \u003cbr /\u003e\n【好きなキャラ】 \u003cbr /\u003e\n【何か一言】\u003c/p\u003e\n\u003cp\u003eこちらをメッセにて送ってください。\u003cbr /\u003e\n名札を作成させていただきます。\u003c/p\u003e\n\u003cp\u003eあと皆さん良い大人だと思いますので必要ないかもしれませんが \u003cbr /\u003e\n一応注意事項です。\u003c/p\u003e\n\u003cp\u003e【注意事項】 \u003cbr /\u003e\n・未成年の参加はダメよー、ダメダメ。\u003cbr /\u003e\n・他人に迷惑をかける行為、悪質な行為は帰っていただきます。その際に返金はいたしません。 \u003cbr /\u003e\n・お酒に自信ニキでも節度ある行動お願いします。 一応終電の確認もオナシャス！！\u003cbr /\u003e\n・アドレスの登録はよく確認するアドレスでお願いします。一定期間、連絡が取れなかった場合はキャンセルとさせていただく場合がございます。\u003cbr /\u003e\n・開催の1週間程前にメッセを送らせていただきます。その後参加者リストの作成に取り掛かります。それまでは仮参加となりますのでご注意ください。\u003c/p\u003e\n\u003cp\u003e何か質問等ございましたら、メッセか\u003cbr /\u003e\ntaconyan39@gmail.com\u003c/p\u003e\n\u003cp\u003eまでお願いします。\u003c/p\u003e\n\u003cp\u003eそれでは当日楽しみましょう！\u003c/p\u003e"",""event_url"":""http://atnd.org/events/60918"",""started_at"":""2020-01-25T19:00:00.000+09:00"",""ended_at"":null,""url"":null,""limit"":20,""address"":""大阪府大阪市北区小松原町5-8　トリオビル４F、5Ｆ"",""place"":""梅田個室空間　楽宴の贈りもの　梅田店"",""lat"":""34.7033395"",""lon"":""135.5014871"",""owner_id"":193806,""owner_nickname"":""たこにゃん"",""owner_twitter_id"":""taconyan39"",""accepted"":6,""waiting"":0,""updated_at"":""2015-03-12T12:27:46.000+09:00""}}]}
";

        static void Main(string[] args)
        {

            var data1 = JsonConvert.DeserializeObject<ConnpassRootData>(connpassJson);

            ObservableCollection<IData> res = new ObservableCollection<IData>();
            foreach (var x in data1.ConnpassEvents)
            {
                //res.Add((IData)x);
            }

        }
    }

    class ConnpassLoader : ILoader
    {
        public async Task<IEnumerable<IData>> LoadAsync(string url)
        {
            using (var client = new HttpClient())
            {
                
            }
            var data = JsonConvert.DeserializeObject<ConnpassRootData>(Program.connpassJson);

            ObservableCollection<IData> res = new ObservableCollection<IData>();
            foreach (var x in data.ConnpassEvents)
            {
                res.Add((IData)x);
            }
            return res;
        }
    }

    class ConnpassRootData
    {
        public ObservableCollection<ConnpassData> ConnpassEvents { get; set; }
    }

    class ConnpassData : IData
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("event_url")]
        public string EventUrl { get; set; }

        [JsonProperty("started_at")]
        public DateTime StartedAt { get; set; }

        [JsonProperty("ended_at")]
        public DateTime EndedAt { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        // place もある。
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("lat")]
        public float Lat { get; set; }

        [JsonProperty("lon")]
        public float Lon { get; set; }

        [JsonProperty("accepted")]
        public int Accepted { get; set; }

        [JsonProperty("waiting")]
        public int Waiting { get; set; }


    }

    interface ILoader
    {
        Task<IEnumerable<IData>> LoadAsync(string url);
    }

    interface IData
    {
        string Title { get; set; }
        string Description { get; set; }
        string EventUrl { get; set; }
        DateTime StartedAt { get; set; }
        DateTime EndedAt { get; set; }
        int Limit { get; set; }
        string Address { get; set; }
        float Lat { get; set; }
        float Lon { get; set; }
        int Accepted { get; set; }
        int Waiting { get; set; }
    }




    #region Connpass

    public class Series
    {
        public string url { get; set; }
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Event
    {
        public string event_url { get; set; }
        public string event_type { get; set; }
        public string owner_nickname { get; set; }
        public Series series { get; set; }
        public string updated_at { get; set; }
        public string lat { get; set; }
        public string started_at { get; set; }
        public string hash_tag { get; set; }
        public string title { get; set; }
        public int event_id { get; set; }
        public string lon { get; set; }
        public int waiting { get; set; }
        public int limit { get; set; }
        public int owner_id { get; set; }
        public string owner_display_name { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string @catch { get; set; }
        public int accepted { get; set; }
        public string ended_at { get; set; }
        public string place { get; set; }
    }

    public class ConnpassJson
    {
        public int results_returned { get; set; }
        public List<Event> events { get; set; }
        public int results_start { get; set; }
        public int results_available { get; set; }
    }

    #endregion


    #region ATND

    //public class Event2
    //{
    //    public int event_id { get; set; }
    //    public string title { get; set; }
    //    public string @catch { get; set; }
    //    public string description { get; set; }
    //    public string event_url { get; set; }
    //    public string started_at { get; set; }
    //    public string ended_at { get; set; }
    //    public string url { get; set; }
    //    public int? limit { get; set; }
    //    public string address { get; set; }
    //    public string place { get; set; }
    //    public string lat { get; set; }
    //    public string lon { get; set; }
    //    public int owner_id { get; set; }
    //    public string owner_nickname { get; set; }
    //    public string owner_twitter_id { get; set; }
    //    public int accepted { get; set; }
    //    public int waiting { get; set; }
    //    public string updated_at { get; set; }
    //}

    //public class Event
    //{
    //    public Event2 @event { get; set; }
    //}

    //public class RootObject
    //{
    //    public int results_returned { get; set; }
    //    public int results_start { get; set; }
    //    public List<Event> events { get; set; }
    //}

    #endregion


    #region Doorkeeper

    //public class Event
    //{
    //    public string title { get; set; }
    //    public int id { get; set; }
    //    public string starts_at { get; set; }
    //    public string ends_at { get; set; }
    //    public string venue_name { get; set; }
    //    public string address { get; set; }
    //    public string lat { get; set; }
    //    public string @long { get; set; }
    //    public int ticket_limit { get; set; }
    //    public string published_at { get; set; }
    //    public string updated_at { get; set; }
    //    public int group { get; set; }
    //    public string description { get; set; }
    //    public string public_url { get; set; }
    //    public int participants { get; set; }
    //    public int waitlisted { get; set; }
    //}

    //public class RootObject
    //{
    //    public Event @event { get; set; }
    //}

    #endregion




    //class Model
    //{
    //    private ILoader[] Loaders { get; } = new ILoader[]
    //    {
    //        //new DoorkeeperLoader(),
    //        //new ConnpassLoader(),
    //        new AtndLoader(),
    //    };
    //    public ObservableCollection<Data> Datas { get; } = new ObservableCollection<Data>();
    //    public async Task LoadAsync()
    //    {
    //        this.Datas.Clear();
    //        foreach (var l in this.Loaders)
    //        {
    //            var result = await l.LoadAsync();
    //            foreach (var x in result)
    //            {
    //                this.Datas.Add(x);
    //            }
    //        }
    //    }
    //}
    //interface ILoader
    //{
    //    Task<IEnumerable<Data>> LoadAsync();
    //}

    ///// <summary>
    ///// See: https://www.doorkeeperhq.com/developer/api
    ///// </summary>
    //class DoorkeeperLoader : ILoader
    //{
    //    public Task<IEnumerable<Data>> LoadAsync()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    ///// <summary>
    ///// See: http://api.atnd.org/
    ///// </summary>
    //class AtndLoader : ILoader
    //{

    //    public async Task<IEnumerable<Data>> LoadAsync()
    //    {
    //        const string baseUrl = "https://api.atnd.org/events/?format=json";
    //        string ymd = "20160830,20160831,20160901,20160902,20160903,20160904,20160905,20160906";
    //        int count = 20;

    //        StringBuilder sb = new StringBuilder();
    //        sb.Append(baseUrl).Append("&ymd=").Append(ymd).Append("&count=").Append(count);

    //        try
    //        {
    //            using (var client = new HttpClient())
    //            {
    //                using (var reader = new StreamReader(await client.GetStreamAsync(sb.ToString())))
    //                {
    //                    var json = await reader.ReadToEndAsync();
    //                    var jsonModel = JsonConvert.DeserializeObject<ObservableCollection<AtndData>>(json);


    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {

    //            throw;
    //        }
    //    }
    //}

    //internal class AtndData
    //{
    //    [JsonProperty("title")]
    //    public string Title { get; set; }

    //}

    ///// <summary>
    ///// See: http://connpass.com/about/api/
    ///// </summary>
    //class ConnpassLoader : ILoader
    //{
    //    public Task<IEnumerable<Data>> LoadAsync()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}






}
