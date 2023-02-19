import AddCircleOutlineOutlinedIcon from '@mui/icons-material/AddCircleOutlineOutlined';

function NewAssetCard() {
  return (
    <div className="card flex flex-col items-center justify-center px-8 py-4 border-2 border-dashed opacity-60 cursor-pointer">
      <h1 className="font-semibold">Create custom asset</h1>
      <AddCircleOutlineOutlinedIcon className="text-4xl" />
    </div>
  );
}

export default NewAssetCard;
