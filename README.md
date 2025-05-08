# rndpwd

A simple Windows command-line tool to generate random passwords and output them to the console.

## Features

- Generate random passwords with customizable length and character sets
- Support for numbers, lowercase, uppercase, symbols, prefix, and no-duplicate characters

## Requirements

- .NET 9.0 (Windows) or later

## Build

1. Clone this repository or copy the source code.
2. Make sure your `.csproj` file contains:

    ```
    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net9.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>
    ```

3. Build with:

    ```
    dotnet build -c Release
    ```

4. The executable will be in `bin\Release\net9.0\`.

## Usage

```
rndpwd [options]
```

### Options

| Option                        | Description                                                         |
|-------------------------------|---------------------------------------------------------------------|
| `--length=<num>`, `-l <num>`  | Length of the password (default: 8)                                 |
| `--numbers`, `-n`             | Include numbers                                                     |
| `--lower-alfabet`, `-la`      | Include lowercase letters                                           |
| `--upper-alfabet`, `-ua`      | Include uppercase letters                                           |
| `--symbol`, `-s`              | Include symbols (default: ``"!#$%&'()*+,-./:;<=>?@[\]^_`{\|}~``)    |
| `--symbol`, `-s <chars>`      | Use the specified symbol characters                                 |
| `--prefix=<char>`, `-p <char>`| Prefix character(s) for the password                                |
| `--no-duplicates`, `-nd`      | Do not allow duplicate characters                                   |

### Examples

- Generate an 8-character password (numbers and lowercase letters by default):
    ```
    rndpwd
    ```

- Generate a 12-character password with numbers, lowercase, uppercase, and symbols, and copy to clipboard:
    ```
    rndpwd -l 12 -n -la -ua -s -c
    ```

- Generate a 10-character password, starting with "X", with no duplicate characters:
    ```
    rndpwd -l 10 -la -n -p X -nd
    ```

## Notes

- If no character set options are specified, numbers and lowercase letters are used by default.
- When using the clipboard option, the application must be run in a Windows environment with clipboard support.
- If you specify `--no-duplicates` and the requested length exceeds the number of unique characters, an error will occur.
