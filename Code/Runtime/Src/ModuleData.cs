using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyFramework
{
    public class ModuleData
    {
        public string ModuleName;
        public string ModuleClass;
        public string ModuleAssembly;
        public string ModuleMode;
        public string ModuleAuthor;
        public string ModuleCreateTime;
        public string ModuleUpdateTime;

        public List<string> ModuleDlls = new List<string>();
        public List<string> ModuleResources = new List<string>();
        public List<string> ModuleEvents = new List<string>();

        public ModuleData()
        {
            
        }

        public void Parse( JObject jObj )
        {
            if( jObj == null )
            {
                return;
            }

            JToken token = null;
            if( jObj.TryGetValue( "Name", out token ) )
            {
                ModuleName = token.ToString();
            }
            
            if (jObj.TryGetValue("AssemblyName", out token))
            {
                ModuleAssembly = token.ToString();
            }

            if (jObj.TryGetValue("Class", out token))
            {
                ModuleClass = token.ToString();
            }

            if (jObj.TryGetValue("Mode", out token))
            {
                ModuleMode = token.ToString();
            }

            if (jObj.TryGetValue("Author", out token))
            {
                ModuleAuthor = token.ToString();
            }

            if (jObj.TryGetValue("CreateTime", out token))
            {
                ModuleCreateTime = token.ToString();
            }

            if (jObj.TryGetValue("UpdateTime", out token))
            {
                ModuleCreateTime = token.ToString();
            }

            if ( jObj.TryGetValue( "Dlls", out token ) )
            {
                JArray array = token.ToObject<JArray>();
                if( array != null )
                {
                    for( int i = 0; i < array.Count; ++i )
                    {
                        string dll = array[i].ToString();

                        if( ModuleDlls != null 
                           && !ModuleDlls.Contains( dll ) )
                        {
                            ModuleDlls.Add( dll );
                        }
                    }
                }
            }

            if (jObj.TryGetValue("Resources", out token))
            {
                JArray array = token.ToObject<JArray>();
                if (array != null)
                {
                    for (int i = 0; i < array.Count; ++i)
                    {
                        string res = array[i].ToString();

                        if ( ModuleResources != null
                           && !ModuleResources.Contains(res) )
                        {
                            ModuleResources.Add(res);
                        }
                    }
                }
            }

            if (jObj.TryGetValue("Events", out token))
            {
                JArray array = token.ToObject<JArray>();
                if (array != null)
                {
                    for (int i = 0; i < array.Count; ++i)
                    {
                        string evt = array[i].ToString();

                        if ( ModuleEvents != null
                           && !ModuleEvents.Contains(evt))
                        {
                            ModuleEvents.Add(evt);
                        }
                    }
                }
            }
        }
    }
}
