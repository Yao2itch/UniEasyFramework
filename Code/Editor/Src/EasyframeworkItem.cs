using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace EasyFrameworkEditor
{
    public class EasyframeworkItem 
    {
        public string ModuleName;
        public string ModuleAssembly;
        public string ModuleClass;
        public string ModuleMode;
        public string ModuleAuthor;
        public string ModuleCreateTime;
        public string ModuleUpdateTime;
        public List<string> ModuleDlls = new List<string>();
        public List<string> ModuleResources = new List<string>();
        public List<string> ModuleEvents = new List<string>();

        public void Parse(JObject jObj)
        {
            if ( jObj == null )
            {
                return;
            }

            JToken token = null;
            
            if ( jObj.TryGetValue( "Name", out token ) )
            {
                ModuleName = token.ToString();
            }
            
            if ( jObj.TryGetValue( "AssemblyName", out token ) )
            {
                ModuleAssembly = token.ToString();
            }
            
            if ( jObj.TryGetValue( "Class", out token ) )
            {
                ModuleClass = token.ToString();
            }
            
            if ( jObj.TryGetValue( "Mode", out token ) )
            {
                ModuleMode = token.ToString();
            }
            
            if ( jObj.TryGetValue( "Author", out token ) )
            {
                ModuleAuthor = token.ToString();
            }
            
            if ( jObj.TryGetValue( "CreateTime", out token ) )
            {
                ModuleCreateTime = token.ToString();
            }
            
            if ( jObj.TryGetValue( "UpdateTime", out token ) )
            {
                ModuleUpdateTime = token.ToString();
            }
            
            if ( jObj.TryGetValue( "Dlls", out token ) )
            {
                JArray jArray = token.ToObject<JArray>();
                if ( jArray != null )
                {
                    for ( int i = 0; i < jArray.Count; ++i )
                    {
                        string strDll = jArray[i].ToString();
                        
                        if ( ModuleDlls != null
                            && !ModuleDlls.Contains( strDll ) )
                        {
                            ModuleDlls.Add(strDll);
                        }
                    }
                }
            }
            
            if ( jObj.TryGetValue( "Resources", out token ) )
            {
                JArray jArray = token.ToObject<JArray>();
                if ( jArray != null )
                {
                    for ( int i = 0; i < jArray.Count; ++i )
                    {
                        string strDll = jArray[i].ToString();
                        
                        if ( ModuleDlls != null
                             && !ModuleDlls.Contains( strDll ) )
                        {
                            ModuleDlls.Add(strDll);
                        }
                    }
                }
            }
            
            if ( jObj.TryGetValue( "Events", out token ) )
            {
                JArray jArray = token.ToObject<JArray>();
                if ( jArray != null )
                {
                    for ( int i = 0; i < jArray.Count; ++i )
                    {
                        string strDll = jArray[i].ToString();
                        
                        if ( ModuleDlls != null
                             && !ModuleDlls.Contains( strDll ) )
                        {
                            ModuleDlls.Add(strDll);
                        }
                    }
                }
            }
        }
    }
}