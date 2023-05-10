import axios from 'axios';
import * as React from 'react';

import { TaxSchemeDto, TaxSchemesClient } from './api';

export default function fetchTaxSchemes(taxSchemes: TaxSchemeDto[] | null, setTaxSchemes: (data: TaxSchemeDto[]) => void) {
  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (taxSchemes === null) {
      const client = new TaxSchemesClient();
      client.getTaxSchemes('1', null, cancelSource.token).then((res) => setTaxSchemes(res.result));
    }

    return () => cancelSource.cancel();
  });

  return [taxSchemes, setTaxSchemes];
}
