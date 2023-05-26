import ECharts from 'echarts-for-react';

import { TimePointDto } from '../../api/api';

interface Props {
  data: TimePointDto[];
}

export default function Charts({ data }: Props) {
  const dates = data.map((timePoint) => timePoint.date.toDateString());

  const seriesOptions = {
    type: 'line',
    smooth: true,
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
    grid: {
      containLabel: true,
    },
    toolbox: { feature: { restore: {} } },
    xAxis: {
      type: 'category',
      name: 'Date',
      data: dates,
    },
    yAxis: {
      type: 'value',
      name: 'Value',
      scale: true,
      axisLabel: { formatter: '{value}€' },
    },
    dataZoom: [
      { type: 'inside', start: 0, end: 100 },
      { start: 0, end: 100 },
    ],
    tooltip: {
      trigger: 'axis',
      valueFormatter: (value: number | string) => (value == null ? '-' : `${Number(value).toFixed(2).toString()}€`),
    },
    series: [netWorth, liquidWorth, assetWorth],
  };

  return <ECharts option={options} style={{ height: '96%', width: '96%' }} />;
}
