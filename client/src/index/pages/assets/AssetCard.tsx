import * as Mui from '@mui/material';

import Card from '../../shared/cards/Card';
import PercentageInput from '../../shared/cards/PercentageInput';

interface Props {
  name: string;
  description: string;
  returnRate: number;
  standardDeviation: number;
  readOnly?: boolean;
}

function AssetCard({ name, description, returnRate, standardDeviation, readOnly }: Props) {
  return (
    <Card>
      <h1 className="font-semibold">{name}</h1>
      <p className="flex-grow pt-2 text-sm break-words">{description}</p>
      <div className="flex flex-row">
        <PercentageInput
          disabled={readOnly}
          label="Real return"
          tooltip="Annual mean real (inflation-adjusted) return of the asset."
          defaultValue={returnRate.toString()}
        />
        <PercentageInput
          disabled={readOnly}
          label="Standard deviation"
          tooltip="Measures the volatility of the asset; how much the return deviates from the annual mean on average."
          defaultValue={standardDeviation.toString()}
        />
      </div>

      <Mui.ButtonGroup className="self-end" variant="text">
        <Mui.Button disabled={readOnly} className="border-0">
          Save
        </Mui.Button>
        <Mui.Button disabled={readOnly} className={`border-0 ${readOnly ? '' : 'text-red-700 hover:bg-red-50'}`}>
          Delete
        </Mui.Button>
      </Mui.ButtonGroup>
    </Card>
  );
}

AssetCard.defaultProps = {
  readOnly: false,
};

export default AssetCard;
