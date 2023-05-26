import * as Mui from '@mui/material';

import { ProjectionDto } from '../../api/api';
import fetchChart from '../../api/fetchChart';
import useCache, { CacheKey } from '../../hooks/useCache';
import Chart from './Chart';

interface Props {
  isOpen: boolean;

  onClose: () => void;
}

export default function ChartPopup({ isOpen, onClose }: Props) {
  const [chart, _setChart] = fetchChart(...useCache<ProjectionDto | null>(CacheKey.Chart, null));

  const loading = <p className="h-min text-3xl">Loading...</p>;

  return (
    <Mui.Dialog
      open={isOpen}
      onClose={onClose}
      fullScreen
      classes={{ paper: 'md:min-w-[70vw] lg:min-w-[65vw] xl:min-w-[38w] 2xl:min-w-[25w]' }}
    >
      <Mui.DialogActions>
        <Mui.Button onClick={onClose} className="text-xl">
          Close
        </Mui.Button>
      </Mui.DialogActions>
      <Mui.DialogContent dividers>
        <div className="flex h-full justify-center items-center text-gray-600">
          {chart === null ? loading : <Chart data={chart.timeSeries} />}
        </div>
      </Mui.DialogContent>
    </Mui.Dialog>
  );
}
