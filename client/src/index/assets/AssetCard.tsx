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
      <p className="flex-grow break-words">{description}</p>
      <AssetPercentageInput disabled={readOnly} label="Real return" defaultValue={returnRate.toString()} />
      <AssetPercentageInput
        disabled={readOnly}
        label="Standard deviation"
        defaultValue={standardDeviation.toString()}
      />
      <Mui.ButtonGroup className="self-end" variant="text">
        <Mui.Button className="border-0">Save</Mui.Button>
        <Mui.Button className="border-0 text-red-600 hover:bg-red-50">Delete</Mui.Button>
      </Mui.ButtonGroup>
    </div>
  );
}

AssetCard.defaultProps = {
  readOnly: false,
};

export default AssetCard;
