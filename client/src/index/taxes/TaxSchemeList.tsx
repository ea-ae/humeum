import NewItemCard from '../../shared/cards/NewItemCard';
import TaxSchemeCard from './TaxSchemeCard';

function TaxSchemeList() {
  return (
    <>
      <TaxSchemeCard />
      <NewItemCard text="Create custom tax scheme" />;
    </>
  );
}

export default TaxSchemeList;
