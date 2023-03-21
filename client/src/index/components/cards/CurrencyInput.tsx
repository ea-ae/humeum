import Input from './Input';

interface Props {
  label: string;
  tooltip?: string;
  defaultValue: string;
  disabled?: boolean;
}

export default function CurrencyInput({ label, tooltip, defaultValue, disabled }: Props) {
  const typePattern = /^[0-9]{0,10}$/;
  const validPattern = /^[0-9]{1,10}$/;

  return (
    <Input
      label={label}
      tooltip={tooltip}
      defaultValue={defaultValue}
      disabled={disabled}
      typePattern={typePattern}
      validPattern={validPattern}
      symbol="€"
    />
  );
}

CurrencyInput.defaultProps = {
  tooltip: '',
  disabled: false,
};
