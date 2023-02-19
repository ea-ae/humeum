import AssetCard from './AssetCard';
import NewAssetCard from './NewAssetCard';

function AssetsList() {
  return (
    <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-3">
      <AssetCard
        name="Index fund"
        description="Index funds track the performance of a particular market index; great diversification, low fees, and easy management."
        returnRate={8.1}
        standardDeviation={15.2}
        readOnly
      />
      <AssetCard
        name="Bond fund"
        description="Bonds funds provide great diversification potential and are generally less volatile than other securities (depending on bond type)."
        returnRate={1.9}
        standardDeviation={3}
        readOnly
      />
      <AssetCard
        name="Custom asset type 1"
        description="Custom asset description goes here."
        returnRate={5}
        standardDeviation={5}
      />
      <AssetCard
        name="Custom asset type 2"
        description={
          'Custom asset description goes here. These descriptions can sometimes get very long, ' +
          'inwhichcasewordwrappingandgridlayoutsshouldtakecareofitwellenough.'
        }
        returnRate={9.4}
        standardDeviation={44.21}
      />
      <AssetCard
        name="Custom asset type 3"
        description="Custom asset description goes here."
        returnRate={1}
        standardDeviation={1}
      />
      <NewAssetCard />
    </div>
  );
}

export default AssetsList;
