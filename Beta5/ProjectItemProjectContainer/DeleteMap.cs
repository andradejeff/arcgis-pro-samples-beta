﻿using System.Collections.Generic;
using System.Linq;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Internal.Mapping;
using ArcGIS.Desktop.Internal.Core;

namespace ProSDKSamples.ProjectItemProjectContainer
{
    internal class DeleteMap : Button
    {
        protected override async void OnClick()
        {
            // get the map container
            var container = ProjectModule.CurrentProject.GetProjectItemContainer<MapContainer>("Map");
            if (container == null)
                return;

            // get the first item
            MapProjectItem item = container.GetProjectItems().FirstOrDefault();
            if (item == null)
                return;

            // fimd the map
            Map map = MappingModule.FindMap(item.Path);
            if (map == null)
                return;

            // close if already open
            IList<IMapPane> mapPanes = MappingModule.GetMapPanes(map);
            if ((mapPanes != null) && (mapPanes.Count > 0))
            {
                for (int idx = mapPanes.Count-1; idx >= 0; idx--)
                {
                    Pane pane = mapPanes[idx] as Pane;
                    pane.Close();
                }
            }

            // remove it from project
            await (ProjectModule.CurrentProject as IInternalGISProjectItem).RemoveProjectItemAsync("Map", item.Path);
        }
    }
}
