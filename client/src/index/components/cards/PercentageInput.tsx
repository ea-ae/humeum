import Input from './Input';

interface Props {
  label: string;
  tooltip?: string;
  defaultValue: string;
  disabled?: boolean;
}

export default function PercentageInput({ label, tooltip, defaultValue, disabled }: Props) {
  const typePattern = /^[0-9]{0,3}($|[,.][0-9]{0,2}$)/;
  const validPattern = /^[0-9]{1,3}($|[,.][0-9]{1,2})$/;

  return (
    <Input
      label={label}
      tooltip={tooltip}
      defaultValue={defaultValue}
      symbol="%"
      typePattern={typePattern}
      validPattern={validPattern}
      disabled={disabled}
    />
  );
}

PercentageInput.defaultProps = {
  tooltip: '',
  disabled: false,
};
