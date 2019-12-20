# Security Policy

## Supported Versions

**NOTE:** Since the project is currently in early development, there is no cycle yet to address any security vulnerabilities found.

| Version | Supported          |
| ------- | ------------------ |
| All     | :x:                |

## Reporting a Vulnerability

Please file an issue in the Issues section of this repo. Prefix the title of the issue with [SECURITY] or [Security Vulnerability] or [Vulnerability] to directly indicate what you are talking about. 

Be clear about what bug you are reporting. 

Some notes:

This project is basically a wrapper around the REST API endpoints exposed by Azure at https://management.azure.com. As such, we will only accept those reports that have to do with CODE IN THIS REPOSITORY and will outright decline any vulnerabilities found with the Azure APIs. 

You are welcome to send PRs with the fixes for (your or someone else's) reported vulnerabilities. Your PR will be accepted IF AND ONLY IF:

1. It meets the design, code and architectural guidelines for the code in this repo.

2. Fixing the problem will NOT deviate from the design, code and architectural guidelines for the code in this repo.

3. Does not reduce performance of existing code.

4. Does not introduce new bugs, whether logical, functional, performance, or security anywhere in the code flow.

If your PR is rejected, you will be told why, in as much detail as possible. There is no "appeal" against this. All decisions by the repo owner [Sujay V Sarma] is final.

