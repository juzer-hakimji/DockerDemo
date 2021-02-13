import { Country } from "./Country";

export class User {
  constructor(
    public FirstName: string,
    public LastName: string,
    public MobileNumber: string,
    public Email: string,
    public Password: string,
    public UserId?: number,
    public Gender?: string,
    public CountryId?: number,
    public LanguageId?: string,
    public Remarks?: string,
    public Token?: string,
    public CountryList?: Country[]
  ) { }
}
