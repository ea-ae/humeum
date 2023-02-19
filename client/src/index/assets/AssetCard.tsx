import * as Mui from '@mui/material';

import AssetPercentageInput from './AssetPercentageInput';

interface Props {
  name: string;
  description: string;
  returnRate: number;
  standardDeviation: number;
  readOnly?: boolean;
}

function AssetCard({ name, description, returnRate, standardDeviation, readOnly }: Props) {
  return (
    <div className="flex flex-col card px-8 py-4">
      <h1 className="font-semibold">{name}</h1>
      <p className="flex-grow pt-2 text-sm break-words">{description}</p>
      <div className="flex flex-row">
        <AssetPercentageInput disabled={readOnly} label="Real return" defaultValue={returnRate.toString()} />
        <AssetPercentageInput
          disabled={readOnly}
          label="Standard deviation"
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
    </div>
  );
}

AssetCard.defaultProps = {
  readOnly: false,
};

export default AssetCard;
