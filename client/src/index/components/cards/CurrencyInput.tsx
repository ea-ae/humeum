import Input from './Input';

interface Props {
  label: string;
  tooltip?: string;
  defaultValue: string; // todo make number
  disabled?: boolean;
  className?: string;
  isOutlined?: boolean;

  onChange?: undefined | ((percentage: number) => void);
}

export default function CurrencyInput({ label, tooltip, defaultValue, disabled, className, isOutlined, onChange }: Props) {
  const typePattern = /^-?[0-9]{0,8}$/;
  const validPattern = /^-?[0-9]{1,8}$/;

  const onInputChange = (input: string) => {
    if (onChange !== null && onChange !== undefined) {
      onChange(parseFloat(input));
    }
  };

  return (
    <Input
      label={label}
      tooltip={tooltip}
      defaultValue={defaultValue}
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
  onChange: undefined,
};
