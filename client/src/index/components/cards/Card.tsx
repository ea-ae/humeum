import * as React from 'react';

interface Props {
  children: React.ReactNode;
  padding?: boolean;
  className?: string;
}

function Card({ children, padding, className }: Props) {
  const paddingClasses = padding ? 'px-8 py-4' : '';
  return <div className={`card flex flex-col ${paddingClasses} ${className}`}>{children}</div>;
}

Card.defaultProps = {
  padding: true,
  className: '',
};

export default Card;
