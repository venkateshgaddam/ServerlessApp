using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Newtonsoft.Json;
using ServerlessApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerlessApp.DAL
{
    public class clsDynamoDB : IDisposable
    {
        static string AccessKey = "AKIAJ6AL3KT4C2ZZAOBQ";//ConfigurationManager<string>.AppSettings["cloudUserAccessKey"];
        static string SecretKey = "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";//ConfigurationManager.AppSettings["cloudUserSecretKey"];
        static string successMessage = string.Empty;
        static int marks = 0;
        static readonly AWSCredentials credentials = new BasicAWSCredentials(AccessKey, SecretKey);
       // AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig();
        AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);

        List<Document> doc = new List<Document>();
        List<Dictionary<string, string>> resultSet = new List<Dictionary<string, string>>();
        public delegate void dTable();
        public delegate int MultiCast(int x, int y);

        public async Task<LoginModel> GetTable()
        {
            Table table;
            try
            {
                table = Table.LoadTable(client, "UserTable");

                //Get Items from DynamoDB
                ScanFilter scanFilter = new ScanFilter();
                scanFilter.AddCondition("Name", ScanOperator.Contains, "Dhoni");
                Search search = table.Scan(scanFilter);

                while (!search.IsDone)
                {
                    doc = await search.GetNextSetAsync();
                }
                string result = "";
                LoginModel loginModel = new LoginModel();

                if (doc.Count > 0)
                {
                    // return true;
                    for (int i = 0; i < doc.Count; i++)
                    {
                        loginModel.Userid = 10;
                        loginModel.UserName = "Dhoni";
                        result = doc[i].ToJson();
                        var arr = doc[i].ToList();
                        //var dc = arr.ToDictionary(arr[0].Key,arr[0].Value);
                        var dicValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                        resultSet.Add(dicValues);
                    }
                }
                return loginModel;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<DataRow>> GetMoviesAsync(string moviename = "")
        {


            try
            {
                Table tblMovie = Table.LoadTable(client, "Movies");
                Search searchMovie = tblMovie.Scan(new ScanFilter());


                var request = new ScanRequest
                {
                    TableName = "Movies",

                };
                if (moviename != "")
                {
                    request.ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":val", new AttributeValue {
                         S = moviename
                     }} };
                    request.FilterExpression = "MovieName = :val";

                }


                var Result = client.ScanAsync(request).Result;

                // httpContext.Session.SetString("tblData", JsonConvert.SerializeObject(Result));

                //  System.Web.HttpContext.Current.SetString("result", JsonConvert.SerializeObject(Result));

                DataTable actors = new DataTable();
                List<Movies> lstMovies = new List<Movies>();
                //actors = await GetActors();


                DataTable datMovies = new DataTable("Movies");
                datMovies.Columns.Add("Movie_Id", typeof(String));
                datMovies.Columns.Add("MovieName", typeof(String));
                datMovies.Columns.Add("Producer", typeof(String));
                datMovies.Columns.Add("DateOfRelease", typeof(String));
                datMovies.Columns.Add("Actors", typeof(List<Actor>));

                for (int i = 0; i < Result.Items.Count; i++)
                {
                    DataRow dr = datMovies.NewRow();
                    dr["Movie_Id"] = 10;
                    dr["MovieName"] = Convert.ToString(Result.Items[i]["MovieName"].S);
                    dr["Producer"] = Convert.ToString(Result.Items[i]["Producer"].S);
                    dr["DateOfRelease"] = Convert.ToString(Result.Items[i]["DateofRelease"].S);

                    dr["Actors"] = GetMovieActors(Result.Items[i]["MovieName"].S.ToString()).Result;

                    datMovies.Rows.Add(dr);
                }

                List<DataRow> list = datMovies.Select().ToList();
                var Results = JsonConvert.SerializeObject(list);


                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<Actor>> GetMovieActors(string movieName)
        {
            DataTable actors = new DataTable();
            actors = await GetActors();

            List<Actor> lstActors = new List<Actor>();
            try
            {
                for (int i = 0; i < actors.Rows.Count; i++)
                {
                    DataRow dataRow = actors.Rows[i];
                    if (dataRow["MovieName"].ToString() == movieName)
                    {
                        Actor actor = new Actor();
                        actor.ActorName = dataRow["ActorName"].ToString();
                        actor.Age = Convert.ToInt32(dataRow["Age"].ToString());
                        actor.MovieName = movieName;
                        lstActors.Add(actor);
                    }
                }

                return lstActors;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DataTable> GetActors()
        {

            try
            {
                Table tblActor = Table.LoadTable(client, "Actors");

                ScanRequest request = new ScanRequest
                {
                    TableName = "Actors"
                    //ExclusiveStartKey = startKey,
                    //ScanFilter = conditions
                };

                // Issue request
                CancellationToken cancellationToken = new CancellationToken();
                ScanResponse response = await client.ScanAsync(request, cancellationToken);

                var Result = client.ScanAsync(request).Result;
                DataTable dtActors = new DataTable("Actors");
                dtActors.Columns.Add("Movie_Id", typeof(int));
                dtActors.Columns.Add("ActorName", typeof(String));
                dtActors.Columns.Add("MovieName", typeof(String));
                dtActors.Columns.Add("Age", typeof(String));


                for (int i = 0; i < Result.Items.Count; i++)
                {
                    DataRow dr = dtActors.NewRow();
                    dr["Movie_Id"] = 10;
                    dr["MovieName"] = Convert.ToString(Result.Items[i]["MovieName"].S);
                    dr["ActorName"] = Convert.ToString(Result.Items[i]["ActorName"].S);
                    dr["Age"] = Convert.ToString(Result.Items[i]["Age"].N);

                    dtActors.Rows.Add(dr);
                }

                List<DataRow> list = dtActors.Select().ToList();
                var Results = JsonConvert.SerializeObject(list);

                return dtActors;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddUser()
        {
            PutItemResponse obj = PutItemAsync().Result;
            return successMessage = obj.HttpStatusCode == System.Net.HttpStatusCode.OK ? "Success" : "Error";
        }

        public async Task<LoginModel> CheckUser(string username, string password)
        {
            try
            {
                LoginModel message = await GetTable();
                return message;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<PutItemResponse> PutItemAsync()
        {
            try
            {
                StringBuilder sb = new StringBuilder("", 50);
                sb.Append("MS Dhoni");
                var request = new PutItemRequest
                {
                    TableName = "UserTable",
                    Item = new Dictionary<string, AttributeValue>()
                          {
                              { "UID", new AttributeValue { S = "5" }},
                              { "Branch", new AttributeValue { S = "CSE" }},
                              { "Marks", new AttributeValue { N=marks.ToString() }},
                              { "Name", new AttributeValue { S = sb.ToString() }},

                          }
                    //  ,
                    //ExpressionAttributeNames = new Dictionary<string, string>() { 

                    //    {"#Student","Name"}                    
                    //},
                    //ExpressionAttributeValues = new Dictionary<string,AttributeValue>(){

                    //    {":studentname",new AttributeValue{S="Dhoni"}}
                    //},
                    //ConditionExpression = "Name = Dhoni"
                };
                return await client.PutItemAsync(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateItem(int id)
        {
            //Update Items
            UpdateItemResponse updateClient = null;

            if (resultSet.Count > 0)
            {
                for (int i = 0; i < resultSet.Count; i++)
                {
                    string attributeValue = "";

                    attributeValue = doc[i]["Id"].AsString();
                    UpdateItemRequest updateRequest = new UpdateItemRequest()
                    {
                        TableName = "Student",
                        Key = new Dictionary<string, AttributeValue> {
                            { "Id",  new AttributeValue { N = attributeValue}}
                        },
                        ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                        {
                           { ":r", new AttributeValue {
                                 N = "10"
                            } },
                            { ":n", new AttributeValue {
                                 N = "1"
                            } }
                        },
                        ConditionExpression = "Marks >= :n",
                        UpdateExpression = "SET Marks = :r",
                        ReturnValues = "UPDATED_NEW"

                    };

                     updateClient = client.UpdateItemAsync(updateRequest).Result;
                }
            }
            successMessage = updateClient.HttpStatusCode == System.Net.HttpStatusCode.OK ? "Item Updated " : "Error Occured While Updating";

        }

        public async Task<string> DeleteItem(string movieName, string successMessage)
        {

            //Delete Item
            DeleteItemRequest deleteReq = new DeleteItemRequest()
            {
                TableName = "Movies",
                Key = new Dictionary<string, AttributeValue>() { { "MovieName", new AttributeValue { S = movieName } } }
            };
            DeleteItemResponse deleteResponse = new DeleteItemResponse();
            deleteResponse = await client.DeleteItemAsync(deleteReq);
            successMessage = deleteResponse.HttpStatusCode == System.Net.HttpStatusCode.OK ? "Item Deleted " : "Error Occured While Delete";
            return successMessage;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
