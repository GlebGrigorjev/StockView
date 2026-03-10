import React, { ChangeEvent, JSX, useState, SyntheticEvent, FormEvent } from 'react'

interface Props {
  onClick: (e: SyntheticEvent) => void;
  search: string  | undefined;
  handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

const SearchBar: React.FC<Props> = (props: Props): JSX.Element => {
  const { onClick, search, handleChange } = props;

  return (
    <div><input value={search} onChange={(e) => handleChange(e)} />

    <button onClick={(e) => onClick(e)} />
    </div>
  );
}

export default SearchBar