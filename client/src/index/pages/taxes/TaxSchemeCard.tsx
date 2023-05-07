import * as React from 'react';

import Card from '../../components/cards/Card';
import CurrencyInput from '../../components/cards/CurrencyInput';
import Input from '../../components/cards/Input';
import PercentageInput from '../../components/cards/PercentageInput';

interface Props {
  name: string;
  description: string;
  taxRate: number;
  discount: number;
  discountAge: number;
  maxIncomePercent: number;
  maxIncome: number;
  readOnly?: boolean;
}

export default function TaxSchemeCard({ name, description, taxRate, discount, discountAge, maxIncomePercent, maxIncome, readOnly }: Props) {
  return (
    <Card>
      <h1 className="font-semibold">{name}</h1>
      <p className="flex-grow mt-2 mb-4 text-stone-700 text-sm break-words">{description}</p>
      <div className="flex flex-row flex-wrap gap-x-8">
        <PercentageInput disabled={readOnly} label="Tax rate" tooltip="The rate at which income is taxed." defaultValue={taxRate} />
        <PercentageInput
          disabled={readOnly}
          label="Tax refund rate"
          tooltip="Income tax refund rate for investments."
          defaultValue={discount}
        />
        <Input
          disabled={readOnly}
          label="Refund age"
          tooltip="Age at which the discount becomes applicable. Set to 0 in case there are no age requirements."
          defaultValue={discountAge.toString()}
          typePattern={/[0-9]{0,2}/}
          validPattern={/[0-9]{1,2}/}
          symbol="y"
        />
        <PercentageInput
          disabled={readOnly}
          label="Refund max income percentage"
          tooltip="Maximum percentage of annual income that is discountable. Set to 100% in case there are no income-based limits."
          defaultValue={maxIncomePercent}
        />
        <CurrencyInput
          disabled={readOnly}
          label="Annual max income sum"
          tooltip="Maximum annual income sum that is discountable. Set to 0 if there is no maximum sum."
          defaultValue={maxIncome}
        />
      </div>
    </Card>
  );
}

TaxSchemeCard.defaultProps = {
  readOnly: false,
};
