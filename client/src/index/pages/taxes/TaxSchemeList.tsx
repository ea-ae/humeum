import * as Mui from '@mui/material';
import * as React from 'react';

import { TaxSchemeDto, TaxSchemesClient } from '../../api/api';
import useCache from '../../hooks/useCache';
import TaxSchemeCard from './TaxSchemeCard';

function TaxSchemeList() {
  const [taxSchemes, setTaxSchemes] = useCache<TaxSchemeDto[] | null>('taxSchemes', null);

  React.useEffect(() => {
    if (taxSchemes === null) {
      const taxSchemeClient = new TaxSchemesClient();
      taxSchemeClient.getTaxSchemes(null).then((res) => setTaxSchemes(res.result));
    }
  }, []);

  let elements: React.ReactElement[];
  if (taxSchemes !== null) {
    elements = taxSchemes.map((taxScheme) => (
      <TaxSchemeCard
        key={taxScheme.id}
        name={taxScheme.name}
        description={taxScheme.description}
        taxRate={taxScheme.taxRate}
        discount={taxScheme.incentiveSchemeTaxRefundRate ?? 0}
        discountAge={taxScheme.incentiveSchemeMinAge ?? 0}
        maxIncome={taxScheme.incentiveSchemeMaxApplicableIncome ?? 0}
        maxIncomePercent={taxScheme.incentiveSchemeMaxIncomePercentage ?? 100}
        readOnly
      />
    ));
  } else {
    elements = Array(4)
      .fill(undefined)
      .map((_, index) => (
        <Mui.Skeleton variant="rectangular" animation="wave" className="bg-primary-150">
          <TaxSchemeCard
            // eslint-disable-next-line react/no-array-index-key
            key={index}
            name="a"
            description={'a'.repeat(165)}
            taxRate={0}
            discount={0}
            discountAge={0}
            maxIncome={0}
            maxIncomePercent={0}
          />
        </Mui.Skeleton>
      ));
  }

  return <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-2">{elements}</div>;
}

export default TaxSchemeList;
