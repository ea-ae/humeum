import TaxSchemeCard from './TaxSchemeCard';

function TaxSchemeList() {
  return (
    <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-2">
      <TaxSchemeCard
        name="Income tax"
        description="Regular flat income tax in Estonia, applicable to all income by default. First 654EUR/mo are tax-free."
        taxRate={20}
        discount={100}
        discountAge={0}
        maxIncome={654 * 12}
        maxIncomePercent={100}
        readOnly
      />
      <TaxSchemeCard
        name="III pillar, post-2021"
        description={
          'Asset income invested through III pillar, with an account opened in 2021 or later. ' +
          'Term pensions based on life expectancy, not included here, provide a 20% discount.'
        }
        taxRate={20}
        discount={10}
        discountAge={60}
        maxIncome={6000}
        maxIncomePercent={15}
        readOnly
      />
      <TaxSchemeCard
        name="III pillar, pre-2021"
        description={
          'Asset income invested through III pillar, with an account opened before 2021. ' +
          'Term pensions based on life expectancy, not included here, provide a 20% discount.'
        }
        taxRate={20}
        discount={10}
        discountAge={55}
        maxIncome={6000}
        maxIncomePercent={15}
        readOnly
      />
      <TaxSchemeCard
        name="Non-taxable income"
        description="Income that due to special circumstances (e.g. charity) is not taxed whatsoever."
        taxRate={0}
        discount={0}
        discountAge={0}
        maxIncome={0}
        maxIncomePercent={0}
        readOnly
      />
    </div>
  );
}

export default TaxSchemeList;
