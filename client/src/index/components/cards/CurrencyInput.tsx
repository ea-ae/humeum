import Input from './Input';

interface Props {
  label: string;
  defaultValue: number;
  tooltip?: string;
  disabled?: boolean;
  className?: string;
  isOutlined?: boolean;

  onChange?: (percentage: number) => void;
}

const typePattern = /^-?[0-9]{0,8}$/;
const validPattern = /^-?[0-9]{1,8}$/;

export default function CurrencyInput({ label, tooltip, defaultValue, disabled, className, isOutlined, onChange }: Props) {
  const onInputChange = (input: string) => {
    if (onChange !== null && onChange !== undefined) {
      onChange(parseFloat(input));
    }
  };

  return (
    <Input
      label={label}
      tooltip={tooltip}
      defaultValue={defaultValue.toString()}
      symbol="€"
      typePattern={typePattern}
      validPattern={validPattern}
      disabled={disabled}
      className={className}
      isOutlined={isOutlined}
      fullWidth
      onChange={onInputChange}
    />
  );
}

CurrencyInput.defaultProps = {
  tooltip: '',
  disabled: false,
  className: '',
  isOutlined: false,
  onChange: (_: number) => null,
};
