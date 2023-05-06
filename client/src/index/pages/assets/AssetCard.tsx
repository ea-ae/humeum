import * as Mui from '@mui/material';
import * as React from 'react';

import Card from '../../components/cards/Card';
import PercentageInput from '../../components/cards/PercentageInput';

interface Props {
  name: string;
  description: string;
  returnRate: number;
  standardDeviation: number;
  readOnly?: boolean;

  onSave?: (realReturn: number, standardDeviation: number) => void;
  onDelete?: () => void;
}

export default function AssetCard({ name, description, returnRate, standardDeviation, readOnly, onSave, onDelete }: Props) {
  const [data, setData] = React.useState<{ returnRate: number; standardDeviation: number }>({ returnRate, standardDeviation });

  const onSaveClick = () => {
    if (onSave !== null && onSave !== undefined) {
      onSave(data.returnRate, data.standardDeviation);
    }
  };

  const onDeleteClick = () => {
    if (onDelete !== null && onDelete !== undefined) {
      onDelete();
    }
  };

  return (
    <Card>
      <h1 className="font-semibold">{name}</h1>
      <p className="flex-grow mt-2 mb-4 text-stone-700 text-sm break-words">{description}</p>
      <div className="flex flex-row">
        <PercentageInput
          disabled={readOnly}
          label="Real return"
          tooltip="Annual mean real (inflation-adjusted) return of the asset."
          defaultValue={returnRate.toString()}
          className="mr-8"
          onChange={(value: number) => setData({ ...data, returnRate: value })}
        />
        <PercentageInput
          disabled={readOnly}
          label="Standard deviation"
          tooltip="Measures the volatility of the asset; how much the return deviates from the annual mean on average."
          defaultValue={standardDeviation.toString()}
          onChange={(value: number) => setData({ ...data, standardDeviation: value })}
        />
      </div>

      <Mui.ButtonGroup className="self-end" variant="text">
        <Mui.Button onClick={onSaveClick} disabled={readOnly} className="border-0">
          Save
        </Mui.Button>
        <Mui.Button onClick={onDeleteClick} disabled={readOnly} className={`border-0 ${readOnly ? '' : 'text-red-700 hover:bg-red-50'}`}>
          Delete
        </Mui.Button>
      </Mui.ButtonGroup>
    </Card>
  );
}

AssetCard.defaultProps = {
  readOnly: false,
  onSave: null,
  onDelete: null,
};
