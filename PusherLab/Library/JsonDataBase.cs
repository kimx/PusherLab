using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PusherLab.Library
{
    public class JsonDataBase
    {
        private string Folder { get; set; }
        private string WordFile { get; set; }

        public JsonDataBase(string folder)
        {
            this.Folder = folder;
            InitWord(folder);
        }

        private void InitWord(string folder)
        {
            WordFile = Path.Combine(folder, "Message.json");
            if (!File.Exists(WordFile))
            {
                File.Create(WordFile).Dispose();
                System.Threading.Thread.Sleep(1000);
            }
            Messages = FileToObject<List<MessageDto>>(WordFile);
            if (Messages == null)
                Messages = new List<MessageDto>();
        }

        public List<MessageDto> Messages { get; set; }

        public void InsertMessage(MessageDto message)
        {
            if (Messages.Count > 0)
                message.ID = Messages.Max(o => o.ID) + 1;
            else
                message.ID = 1;
            Messages.Add(message);
            SaveChanges();
        }

        public MessageDto UpdateMessageByRead(int id)
        {
            var message = Messages.Find(o => o.ID == id);
            message.Readed = true;
            SaveChanges();
            return message;
        }

        public List<MessageDto> UnReadMessages(string channel)
        {
            return Messages.Where(o => o.Readed == false && o.Channel == channel).ToList();
        }

        private void SaveChanges()
        {
            ObjectToFile(Messages, WordFile);
        }

        /// <summary>
        /// 序列化物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private void ObjectToFile<T>(T value, string file)
        {

            using (StreamWriter sw = new StreamWriter(file))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.NullValueHandling = NullValueHandling.Ignore;//2015/05/04
                    serializer.Serialize(writer, value);
                }
            }

        }

        private T FileToObject<T>(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(reader);
                }
            }
        }
    }

    public class MessageDto
    {
        public int ID { get; set; }

        public string Channel { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public bool Readed { get; set; }
    }
}