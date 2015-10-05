// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public abstract class Feature : Object
    {
        
        //<!-- inherited from Feature element -->
        //<name>...</name>                      <!-- string -->
        //<visibility>1</visibility>            <!-- boolean -->
        //<open>0</open>                        <!-- boolean -->
        //<atom:author>...<atom:author>         <!-- xmlns:atom -->
        //<atom:link href=" "/>                 <!-- xmlns:atom -->
        //<address>...</address>                <!-- string -->
        //<xal:AddressDetails>...</xal:AddressDetails>  <!-- xmlns:xal -->
        //<phoneNumber>...</phoneNumber>        <!-- string -->
        //<Snippet maxLines="2">...</Snippet>   <!-- string -->
        //<description>...</description>        <!-- string -->
        //<AbstractView>...</AbstractView>      <!-- Camera or LookAt -->
        //<TimePrimitive>...</TimePrimitive>
        //<styleUrl>...</styleUrl>              <!-- anyURI -->
        //<StyleSelector>...</StyleSelector>
        //<Region>...</Region>
        //<Metadata>...</Metadata>              <!-- deprecated in KML 2.2 -->
        //<ExtendedData>...</ExtendedData>     

        //<!-- specific to Folder -->
        //<!-- 0 or more Feature elements -->

        public string name;
        //public bool visibility;
        //public bool open;

        //string author;
        //string link;
        //string address;
        //string AddressDetails;
        //string phoneNumber;
        //string snippet;
        //int snippetMaxLines;
        //string description;
        // AbstractView abstractView;
        //TimePrimitive timePrimitive;
        //string styleUrl;
        // StyleSelector styleSelector;
        // Region region;
        // ExtendedData extendedData;
        // Feature[] features;
        public override abstract XNode ToXNode();
    }
}

// CLASSIFICATION: UNCLASSIFIED