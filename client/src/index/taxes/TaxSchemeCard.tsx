import * as Mui from '@mui/material';

import Card from '../../shared/cards/Card';
import PercentageInput from '../../shared/cards/PercentageInput';

interface Props {
  name: string;
  description: string;
  taxRate: number;
  discount: number;
  discountAge: number;
  discountMaxIncome: number;
  readOnly?: boolean;
}

function TaxSchemeCard({ name, description, taxRate, discount, discountAge, discountMaxIncome, readOnly }: Props) {
  return (
    <Card>
      <h1 className="font-semibold">{name}</h1>
      <p className="flex-grow pt-2 text-sm break-words">{description}</p>
      <div className="flex flex-row">
        <PercentageInput disabled={readOnly} label="Tax rate" tooltip="Tax rates" defaultValue={taxRate.toString()} />
        <PercentageInput disabled={readOnly} label="Tax refund" defaultValue={discount.toString()} />
        <PercentageInput disabled={readOnly} label="Refund age" defaultValue={discountAge.toString()} />
        <PercentageInput disabled={readOnly} label="Refund max income" defaultValue={discountMaxIncome.toString()} />
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

TaxSchemeCard.defaultProps = {
  readOnly: false,
};

export default TaxSchemeCard;
