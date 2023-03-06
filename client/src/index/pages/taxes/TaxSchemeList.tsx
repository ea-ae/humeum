import * as React from 'react';

import { TaxSchemeDto, TaxSchemesClient } from '../../api/api';
import useCache from '../../hooks/useCache';
import TaxSchemeCard from './TaxSchemeCard';

function TaxSchemeList() {
  const [taxSchemes, setTaxSchemes, _] = useCache<TaxSchemeDto[] | null>('taxSchemes', null);

  React.useEffect(() => {
    if (taxSchemes === null) {
      const taxSchemeClient = new TaxSchemesClient();
      taxSchemeClient.getTaxSchemes(null).then((res) => setTaxSchemes(res.result));
    }
  }, []);

  return (
    <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-2">
      {taxSchemes?.map((taxScheme) => (
        <TaxSchemeCard
          name={taxScheme.name}
          description={taxScheme.description}
          taxRate={taxScheme.taxRate}
          discount={taxScheme.incentiveSchemeTaxRefundRate ?? 0}
          discountAge={taxScheme.incentiveSchemeMinAge ?? 0}
          maxIncome={taxScheme.incentiveSchemeMaxApplicableIncome ?? 0}
          maxIncomePercent={taxScheme.incentiveSchemeMaxIncomePercentage ?? 100}
          readOnly
        />
      ))}
    </div>
  );
}

export default TaxSchemeList;
