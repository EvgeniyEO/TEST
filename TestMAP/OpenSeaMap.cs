using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMAP
{
        using System;
        using GMap.NET.Projections;
        using System.Globalization;
        using GMap.NET.MapProviders;
        using GMap.NET;
        using System.Reflection;

        public class OpenSeaMap : OpenStreetMapProviderBase
        {
            public static readonly OpenSeaMap Instance;

            OpenSeaMap()
            {
                RefererUrl = "http://openseamap.org/";
            }

            static OpenSeaMap()
            {
                Instance = new OpenSeaMap();
            }

            #region GMapProvider Members

            readonly Guid id = new Guid("FAACDE73-4B90-4AE6-BB4A-ADE4F3545592");
            public override Guid Id
            {
                get
                {
                    return id;
                }
            }

            readonly string name = "OpenSeaMap";
            public override string Name
            {
                get
                {
                    return name;
                }
            }

            GMapProvider[] overlays;
            public override GMapProvider[] Overlays
            {
                get
                {
                    if (overlays == null)
                    {
                        overlays = new GMapProvider[] { OpenStreetMapProvider.Instance, this };
                    }
                    return overlays;
                }
            }

            public override PureImage GetTileImage(GPoint pos, int zoom)
            {
                string url = MakeTileImageUrl(pos, zoom, string.Empty);

                return GetTileImageUsingHttp(url);
            }

            #endregion

            string MakeTileImageUrl(GPoint pos, int zoom, string language)
            {
                return string.Format(UrlFormat, zoom, pos.X, pos.Y);
            }

            static readonly string UrlFormat = "http://tiles.openseamap.org/seamark/{0}/{1}/{2}.png";
        }
}
