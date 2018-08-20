/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;

namespace QuantConnect.Util
{
    /// <summary>
    /// Json Converter for Series which handles special Pie Series serialization case
    /// </summary>
    public class SeriesJsonConverter : TypeChangeJsonConverter<Series, BaseSeries>
    {
        protected override BaseSeries Convert(Series value)
        {
            var values = value.Values;
            if (value.SeriesType == SeriesType.Pie)
            {
                values = new List<ChartPoint>();
                var dataPoint = value.ConsolidateChartPoints();
                if (dataPoint != null)
                {
                    values.Add(dataPoint);
                }
            }

            return new BaseSeries
            {
                Color = value.Color,
                Values = values,
                Name = value.Name,
                SeriesType = value.SeriesType,
                ScatterMarkerSymbol = value.ScatterMarkerSymbol,
                Unit = value.Unit,
                Index = value.Index
            };
        }

        protected override Series Convert(BaseSeries value)
        {
            throw new NotImplementedException();
        }
    }
}
