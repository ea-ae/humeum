import * as Mui from '@mui/material';

import Card from '../../components/cards/Card';
import CurrencyInput from '../../components/cards/CurrencyInput';
import PercentageInput from '../../components/cards/PercentageInput';

export default function SettingsPage() {
  return (
    <div className="">
      <Card className="inline-flex max-w-min mr-4 mb-4">
        <h1 className="mb-4 text-xl">Profile Settings</h1>
        <Mui.Button className="mb-4 px-10 whitespace-nowrap" variant="outlined">
          Download profile data
        </Mui.Button>
        <Mui.Button className="mb-4 px-10 bg-red-700 whitespace-nowrap" variant="contained">
          Delete profile data
        </Mui.Button>
        <p className="text-sm text-red-700 text-center">
          Warning: This action is irreversible. Your profile will be deleted and you will lose all your data.
        </p>
      </Card>
      <Card className="inline-flex max-w-fit">
        <h1 className="mb-4 text-xl">Financial Settings</h1>
        <CurrencyInput
          label="Pre-existing savings"
          tooltip="The amount of money you have saved up by start of transaction tracking."
          defaultValue={0}
        />
        <PercentageInput
          label="Retirement withdrawal rate"
          tooltip={
            'Annual fund withdrawal rate, inflation-adjusted. The traditional 4% rule works well for ' +
            'traditional 30-year retirement windows (at 65). A conservative 3-3.5% is often ' +
            'recommended for early retirees by financial experts pessimistic about market crashes.'
          }
          defaultValue={3.5}
        />
      </Card>
    </div>
  );
}
