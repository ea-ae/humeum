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

function TaxSchemeCard(props: Props) {
  const { name, description, taxRate, discount, discountAge, maxIncomePercent, maxIncome, readOnly } = props;

  return (
    <Card>
      <h1 className="font-semibold">{name}</h1>
      <p className="flex-grow pt-2 text-sm break-words">{description}</p>
      <div className="flex flex-row flex-wrap">
        <PercentageInput
          disabled={readOnly}
          label="Tax rate"
          tooltip="The rate at which income is taxed."
          defaultValue={taxRate.toString()}
        />
        <PercentageInput
          disabled={readOnly}
          label="Tax refund rate"
          tooltip="Income tax refund rate for investments."
          defaultValue={discount.toString()}
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
          defaultValue={maxIncomePercent.toString()}
        />
        <CurrencyInput
          disabled={readOnly}
          label="Annual max income sum"
          tooltip="Maximum annual income sum that is discountable. Set to 0 if there is no maximum sum."
          defaultValue={maxIncome.toString()}
        />
        {/* <Mui.ButtonGroup className="self-center my-4" variant="text">
          <Mui.Button disabled={readOnly} className="border-0">
            Save
          </Mui.Button>
          <Mui.Button disabled={readOnly} className={`border-0 ${readOnly ? '' : 'text-red-700 hover:bg-red-50'}`}>
            Delete
          </Mui.Button>
        </Mui.ButtonGroup> */}
      </div>
    </Card>
  );
}

TaxSchemeCard.defaultProps = {
  readOnly: false,
};

export default TaxSchemeCard;
