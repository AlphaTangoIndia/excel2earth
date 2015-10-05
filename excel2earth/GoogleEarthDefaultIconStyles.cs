// CLASSIFICATION: UNCLASSIFIED

using System.Runtime.InteropServices;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class GoogleEarthDefaultIconStyles
    {
        public Style[] styles;

        public GoogleEarthDefaultIconStyles()
        {
            this.styles = new Style[]
            {
                new Style("ylw-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("blue-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("grn-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("ltblu-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/ltblu-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("pink-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/pink-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("purple-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/purple-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("red-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/red-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("wht-pushpin", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/pushpin/wht-pushpin.png"), new IconStyle.vec2(20d, 2d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("A", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/A.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/A-lv.png"))),
                new Style("B", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/B.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/B-lv.png"))),
                new Style("C", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/C.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/C-lv.png"))),
                new Style("D", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/D.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/D-lv.png"))),
                new Style("E", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/E.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/E-lv.png"))),
                new Style("F", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/F.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/F-lv.png"))),
                new Style("G", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/G.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/G-lv.png"))),
                new Style("H", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/H.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/H-lv.png"))),
                new Style("I", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/I.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/I-lv.png"))),
                new Style("J", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/J.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/J-lv.png"))),
                new Style("K", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/K.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/K-lv.png"))),
                new Style("L", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/L.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/L-lv.png"))),
                new Style("M", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/M.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/M-lv.png"))),
                new Style("N", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/N.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/N-lv.png"))),
                new Style("O", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/O.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/O-lv.png"))),
                new Style("P", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/P.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/P-lv.png"))),
                new Style("Q", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/Q.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/Q-lv.png"))),
                new Style("R", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/R.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/R-lv.png"))),
                new Style("S", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/S.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/S-lv.png"))),
                new Style("T", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/T.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/T-lv.png"))),
                new Style("U", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/U.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/U-lv.png"))),
                new Style("V", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/V.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/V-lv.png"))),
                new Style("W", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/W.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/W-lv.png"))),
                new Style("X", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/X.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/X-lv.png"))),
                new Style("Y", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/Y.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/Y-lv.png"))),
                new Style("Z", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/Z.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/Z-lv.png"))),
                new Style("1", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/1.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/1-lv.png"))),
                new Style("2", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/2.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/2-lv.png"))),
                new Style("3", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/3.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/3-lv.png"))),
                new Style("4", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/4.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/4-lv.png"))),
                new Style("5", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/5.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/5-lv.png"))),
                new Style("6", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/6.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/6-lv.png"))),
                new Style("7", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/7.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/7-lv.png"))),
                new Style("8", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/8.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/8-lv.png"))),
                new Style("9", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/9.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/9-lv.png"))),
                new Style("10", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/10.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/10-lv.png"))),
                new Style("blu-blank", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/blu-blank.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/blu-blank-lv.png"))),
                new Style("blu-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/blu-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/blu-diamond-lv.png"))),
                new Style("blu-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/blu-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/blu-circle-lv.png"))),
                new Style("blu-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/blu-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/blu-square-lv.png"))),
                new Style("blu-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/blu-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/blu-stars-lv.png"))),
                new Style("grn-blank", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/grn-blank.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/grn-blank-lv.png"))),
                new Style("grn-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/grn-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/grn-diamond-lv.png"))),
                new Style("grn-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/grn-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/grn-circle-lv.png"))),
                new Style("grn-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/grn-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/grn-square-lv.png"))),
                new Style("grn-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/grn-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/grn-stars-lv.png"))),
                new Style("ltblu-blank", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ltblu-blank.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ltblu-blank-lv.png"))),
                new Style("ltblu-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ltblu-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ltblu-diamond-lv.png"))),
                new Style("ltblu-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ltblu-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ltblu-circle-lv.png"))),
                new Style("ltblu-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ltblu-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ltblu-square-lv.png"))),
                new Style("ltblu-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ltblu-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ltblu-stars-lv.png"))),
                new Style("pink-blank", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/pink-blank.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/pink-blank-lv.png"))),
                new Style("pink-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/pink-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/pink-diamond-lv.png"))),
                new Style("pink-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/pink-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/pink-circle-lv.png"))),
                new Style("pink-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/pink-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/pink-square-lv.png"))),
                new Style("pink-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/pink-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/pink-stars-lv.png"))),
                new Style("ylw-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ylw-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ylw-diamond-lv.png"))),
                new Style("ylw-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ylw-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ylw-circle-lv.png"))),
                new Style("ylw-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ylw-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ylw-square-lv.png"))),
                new Style("ylw-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/ylw-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/ylw-stars-lv.png"))),
                new Style("wht-blank", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/wht-blank.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/wht-blank-lv.png"))),
                new Style("wht-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/wht-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/wht-diamond-lv.png"))),
                new Style("wht-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/wht-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/wht-circle-lv.png"))),
                new Style("wht-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/wht-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/wht-square-lv.png"))),
                new Style("wht-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/wht-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/wht-stars-lv.png"))),
                new Style("red-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/red-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/red-diamond-lv.png"))),
                new Style("red-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/red-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/red-circle-lv.png"))),
                new Style("red-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/red-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/red-square-lv.png"))),
                new Style("red-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/red-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/red-stars-lv.png"))),
                new Style("purple-diamond", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/purple-diamond.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/purple-diamond-lv.png"))),
                new Style("purple-circle", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/purple-circle.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/purple-circle-lv.png"))),
                new Style("purple-square", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/purple-square.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/purple-square-lv.png"))),
                new Style("purple-stars", new IconStyle(1.1f, new Icon("http://maps.google.com/mapfiles/kml/paddle/purple-stars.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels)), new ListStyle(new ListStyle.ItemIcon("http://maps.google.com/mapfiles/kml/paddle/purple-stars-lv.png"))),
                new Style("arrow-reverse", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/arrow-reverse.png"), new IconStyle.vec2(54d, 42d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("arrow", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/arrow.png"), new IconStyle.vec2(32d, 1d, IconStyle.vec2.unitsEnum.pixels, IconStyle.vec2.unitsEnum.pixels))),
                new Style("donut", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/donut.png"))),
                new Style("forbidden", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/forbidden.png"))),
                new Style("info-i", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/info-i.png"))),
                new Style("polygon", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/polygon.png"))),
                new Style("open-diamond", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/open-diamond.png"))),
                new Style("square", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/square.png"))),
                new Style("star", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/star.png"))),
                new Style("target", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/target.png"))),
                new Style("triangle", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/triangle.png"))),
                new Style("cross-hairs", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/cross-hairs.png"))),
                new Style("placemark_square", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/placemark_square.png"))),
                new Style("placemark_circle", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png"))),
                new Style("shaded_dot", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/shaded_dot.png"))),
                new Style("dining", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/dining.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("coffee", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/coffee.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("bars", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/bars.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("snack_bar", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/snack_bar.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("man", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/man.png"))),
                new Style("woman", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/woman.png"))),
                new Style("wheel_chair_accessible", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/wheel_chair_accessible.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("parking_lot", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/parking_lot.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("cabs", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/cabs.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("bus", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/bus.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("truck", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/truck.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("rail", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/rail.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("airports", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/airports.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("ferry", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/ferry.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("heliport", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/heliport.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("subway", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/subway.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("tram", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/tram.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("info", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/info.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("info_circle", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/info_circle.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("flag", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/flag.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("rainy", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/rainy.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("water", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/water.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("snowflake_simple", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/snowflake_simple.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("marina", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/marina.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("fishing", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/fishing.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("sailing", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/sailing.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("swimming", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/swimming.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("ski", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/ski.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("parks", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/parks.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("campfire", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/campfire.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("picnic.", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/picnic.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("campground", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/campground.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("ranger_station", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/ranger_station.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("toilets", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/toilets.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("poi", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/poi.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("hiker", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/hiker.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("cycling", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/cycling.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("motorcycling", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/motorcycling.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("horsebackriding", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/horsebackriding.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("play", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/play.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("golf", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/golf.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("trail", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/trail.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("shopping", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/shopping.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("movies", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/movies.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("convenience", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/convenience.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("grocery.", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/grocery.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("arts", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/arts.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("homegardenbusiness", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/homegardenbusiness.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("electronics", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/electronics.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("mechanic", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/mechanic.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("gas_stations", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/gas_stations.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("realestate", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/realestate.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("salon", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/salon.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("dollar", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/dollar.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("euro", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/euro.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("yen", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/yen.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("firedept", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/firedept.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("hospitals", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/hospitals.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("lodging", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/lodging.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("phone", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/phone.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("caution", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/caution.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("earthquake", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/earthquake.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("falling_rocks", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/falling_rocks.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("post_office", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/post_office.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("police", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/police.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("sunny", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/sunny.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("partly_cloudy", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/partly_cloudy.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("volcano", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/volcano.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("camera", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/camera.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction))),
                new Style("webcam", new IconStyle(1.2f, new Icon("http://maps.google.com/mapfiles/kml/shapes/webcam.png"), new IconStyle.vec2(0.5d, 0d, IconStyle.vec2.unitsEnum.fraction, IconStyle.vec2.unitsEnum.fraction)))
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Style getStyle(int i, StyleMap.Pair.styleStateEnum key)
        {
            Style style = new Style(this.styles[i].id, new IconStyle(this.styles[i].iconStyle.scale, this.styles[i].iconStyle.icon, this.styles[i].iconStyle.hotSpot));

            if (key == StyleMap.Pair.styleStateEnum.normal)
            {
                style.id = "sn_" + style.id;
            }
            else
            {
                style.id = "sh_" + style.id;
                style.iconStyle.scale += 0.2f;
            }

            return style;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public StyleMap getStyleMap(int index)
        {
            return new StyleMap("msn_" + this.styles[index].id, new StyleMap.Pair(StyleMap.Pair.styleStateEnum.normal, this.getStyle(index, StyleMap.Pair.styleStateEnum.normal)), new StyleMap.Pair(StyleMap.Pair.styleStateEnum.highlight, this.getStyle(index, StyleMap.Pair.styleStateEnum.highlight)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string getStyleUrl(int index)
        {
            return "#msn_" + this.styles[index].id;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED