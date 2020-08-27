using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TestClient.Test.NewtonsoftTest
{


    public class NewtonsoftTest
    {


        [NUnit.Framework.Test]
        public void Test()
        {

            StringBuilder stringBuilder = new StringBuilder();
            Newtonsoft.Json.JsonTextWriter jsonWriter = new JsonTextWriter(new StringWriter(stringBuilder));

            jsonWriter.WriteStartObject();
            jsonWriter.Formatting = Formatting.Indented;

            jsonWriter.WritePropertyName("name");
            jsonWriter.WriteValue(false);

            jsonWriter.WritePropertyName("name1");
            JsonSerializer serializer = JsonSerializer.Create();
            jsonWriter.WriteRawValue(JsonConvert.SerializeObject(new TestInterface.IStudentService.Student() { Name = "fgq" }));

            jsonWriter.WritePropertyName("name2");
            jsonWriter.WriteValue(string.Empty);


            jsonWriter.WritePropertyName("name3");           


            // jsonWriter.WriteRaw("{}");

            jsonWriter.WriteEndObject();


            ///-------------------
            jsonWriter.Flush();
            System.Console.WriteLine("value:" + stringBuilder.ToString());
        }
    }
}