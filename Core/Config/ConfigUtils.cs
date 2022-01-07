using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBddAutomationFramework.Core.Config
{
    class ConfigUtils
    {
        /*
         
         This class contains methods related to fetch data from config file
         This class also contains methods to fetch baseurl and service Description
        
        */

        String projectPath;
        
        public ConfigUtils() 
        {
            
            // This will get the current PROJECT directory upto \bin\Debug 
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            //the project directory is the grand-father of the current directory
            projectPath = currentDirectory.Parent.Parent.FullName;

        }
        
        public Dictionary<String, String>  GetValueFromJsonKeyPath(String jsonFilePath, String KeyPath) 
        {
            
            /* Description:
             * This method fetch value of the key from JSON file 
             * And return List
             */

            try
            {
                //read file from path and parse into json object 
                JObject jsonSDFileData = JObject.Parse(File.ReadAllText(jsonFilePath));
                //get json keypath object and convert into dictonary
                Dictionary<String, String> serviceParams = jsonSDFileData[KeyPath].ToObject<Dictionary<String,String>>();
                return serviceParams;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error in GetValuefromJsonKeypath method--> "+ ex.Message);
                throw;
            }

        }

        public String FetchServiceDescriptionPath()
        {
            /*
             * Description:
             * This method fetch path of {Project}/Services/serviceDescription directory
             */
            
            try
            {
                String serviceDescpath = Path.Combine(projectPath, "Services", "ServiceDescription");
                return serviceDescpath;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in FetchServiceDescriptionPath method" + ex.Message);
                throw;
            }
        }

        public String FetchServicePayloadPath() 
        {
            
            /* Description:
             * This method fetch path of {Project}/Services/ServiceDescription Directory
             */
            
            try
            {
                String servicePayloadsPath = Path.Combine(projectPath, "Services", "Payload");
                return servicePayloadsPath;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in FetchServicePayload method-->" + ex.Message);
                throw;
            
            }
        }

        public Dictionary<String, String> GetServiceDescription(String serviceDescRelFilePath, String KeyPath)
        {
            /*
             * Description:
                |  This method fetches entire service description params of a particular from the service description json file.
                |  The service description json file contains below keys
                |  method:
                |  targetURL:
                |  endpoint:
                |  queryparams:
                |  headers:
                |  authType:
                |  username:
                |  password:
                |  payload:                

                :param serviceDescRelFilePath: Relative path of the service description file      
                :param keyPath:

                :return: Dictionary
             */
            try
            {
                Dictionary<String,String> dictServiceDesc = new Dictionary<String,String>();
                String serviceDescPath = Path.Combine(FetchServiceDescriptionPath(), serviceDescRelFilePath);
                Dictionary<String, String> dictServiceDescription = GetValueFromJsonKeyPath(serviceDescPath, KeyPath);

                dictServiceDesc["method"] = dictServiceDescription["method"];
                dictServiceDesc["targetURL"] = dictServiceDescription["targetURL"];
                dictServiceDesc["endpoint"] = dictServiceDescription["endpoint"];

                if (dictServiceDescription["queryparams"] == "None")
                    dictServiceDesc["queryparams"] = "";
                else
                    dictServiceDesc["queryparams"] = dictServiceDescription["queryparams"];

                if (dictServiceDescription["headers"] == "None")
                    dictServiceDesc["headers"] = "{ }";
                else
                    dictServiceDesc["headers"] = dictServiceDescription["headers"];

                dictServiceDesc["authType"] = dictServiceDescription["authType"];
                dictServiceDesc["username"] = dictServiceDescription["username"];
                dictServiceDesc["password"] = dictServiceDescription["password"];

                if (dictServiceDescription["payload"] == "None")
                    dictServiceDesc["payload"] = "";
                else
                {
                    String payloadPath = Path.Combine(FetchServicePayloadPath(), dictServiceDescription["payload"]);
                    String jsonPayload = File.ReadAllText(payloadPath);
                    dictServiceDesc["payload"] = jsonPayload;
                }

                return dictServiceDesc;


            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error appearing in GetServiceDescription method-->" + ex.Message);
                throw;
            }
        }
    }
}
