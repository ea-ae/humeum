import * as Mui from '@mui/material';
import * as React from 'react';

import Card from '../../components/cards/Card';
import ChartPopup from './ChartPopup';

export default function StatisticsCard() {
  const [isChartOpen, setIsChartOpen] = React.useState<boolean>(false);

  return (
    <Card className="max-w-[25rem] overflow-auto">
      <h1 className="min-w-[15rem] mb-4 text-xl">Statistics</h1>
      <p className="min-w-[15rem] mb-4">View your projected net worth based on your current transactions and assets.</p>
      <Mui.Button onClick={() => setIsChartOpen(true)} variant="outlined" className="whitespace-nowrap">
        View chart
      </Mui.Button>
      <ChartPopup isOpen={isChartOpen} onClose={() => setIsChartOpen(false)} />
    </Card>
  );
}
