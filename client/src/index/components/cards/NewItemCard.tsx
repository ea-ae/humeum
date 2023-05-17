import AddCircleOutlineOutlinedIcon from '@mui/icons-material/AddCircleOutlineOutlined';

interface Props {
  text: string;

  onClick: () => void;
}

export default function NewItemCard({ text, onClick }: Props) {
  return (
    <button
      type="button"
      onClick={onClick}
      className="card flex flex-col items-center justify-center px-8 py-4 border-2 border-dashed opacity-60 cursor-pointer"
    >
      <h1 className="font-semibold">{text}</h1>
      <AddCircleOutlineOutlinedIcon className="text-4xl" />
    </button>
  );
}
