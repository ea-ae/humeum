import ECharts, { EChartsOption } from 'echarts-for-react';

import { TimePointDto } from '../../api/api';

interface Props {
  data: TimePointDto[];
}

export default function Charts({ data }: Props) {
  const dates = data.map((timePoint) => timePoint.date.toDateString());

  const seriesOptions = {
    type: 'line',
  };

  const netWorth = {
    ...seriesOptions,
    name: 'Net worth',
    data: data.map((timePoint) => timePoint.liquidWorth + timePoint.assetWorth),
  };

  const liquidWorth = {
    ...seriesOptions,
    name: 'Liquid worth',
    data: data.map((timePoint) => timePoint.liquidWorth),
  };

  const assetWorth = {
    ...seriesOptions,
    name: 'Asset worth',
    data: data.map((timePoint) => timePoint.assetWorth),
  };

  const options = {
    grid: { top: 8, right: 8, bottom: 70, left: 40 },
    toolbox: { feature: { restore: {} } },
    xAxis: {
      type: 'category',
      name: 'Date',
      data: dates,
      // boundaryGap: true,
    },
    yAxis: {
      type: 'value',
      name: 'Value',
      scale: true,
      // boundaryGap: [0, '100%'],
      axisLabel: { formatter: '{value}€' },
    },
    // dataZoom: [
    //   { type: 'inside', start: 0, end: 100 },
    //   { start: 0, end: 100 },
    // ],
    tooltip: {
      trigger: 'axis',
      valueFormatter: (value: number | string) => (value == null ? '-' : `${Number(value).toFixed(2).toString()}€`),
    },
    series: [netWorth, liquidWorth, assetWorth],
  };

  return <ECharts option={options} style={{ width: '100%' }} />;
}
