<p align="center"><a href="https://laravel.com" target="_blank"><img src="https://raw.githubusercontent.com/laravel/art/master/logo-lockup/5%20SVG/2%20CMYK/1%20Full%20Color/laravel-logolockup-cmyk-red.svg" width="400"></a></p>

<p align="center">
<a href="https://travis-ci.org/laravel/framework"><img src="https://travis-ci.org/laravel/framework.svg" alt="Build Status"></a>
<a href="https://packagist.org/packages/laravel/framework"><img src="https://img.shields.io/packagist/dt/laravel/framework" alt="Total Downloads"></a>
<a href="https://packagist.org/packages/laravel/framework"><img src="https://img.shields.io/packagist/v/laravel/framework" alt="Latest Stable Version"></a>
<a href="https://packagist.org/packages/laravel/framework"><img src="https://img.shields.io/packagist/l/laravel/framework" alt="License"></a>
</p>

## About Laravel

Laravel is a web application framework with expressive, elegant syntax. We believe development must be an enjoyable and creative experience to be truly fulfilling. Laravel takes the pain out of development by easing common tasks used in many web projects, such as:

- [Simple, fast routing engine](https://laravel.com/docs/routing).
- [Powerful dependency injection container](https://laravel.com/docs/container).
- Multiple back-ends for [session](https://laravel.com/docs/session) and [cache](https://laravel.com/docs/cache) storage.
- Expressive, intuitive [database ORM](https://laravel.com/docs/eloquent).
- Database agnostic [schema migrations](https://laravel.com/docs/migrations).
- [Robust background job processing](https://laravel.com/docs/queues).
- [Real-time event broadcasting](https://laravel.com/docs/broadcasting).

Laravel is accessible, powerful, and provides tools required for large, robust applications.

## Learning Laravel

Laravel has the most extensive and thorough [documentation](https://laravel.com/docs) and video tutorial library of all modern web application frameworks, making it a breeze to get started with the framework.

If you don't feel like reading, [Laracasts](https://laracasts.com) can help. Laracasts contains over 1500 video tutorials on a range of topics including Laravel, modern PHP, unit testing, and JavaScript. Boost your skills by digging into our comprehensive video library.

## Laravel Sponsors

We would like to extend our thanks to the following sponsors for funding Laravel development. If you are interested in becoming a sponsor, please visit the Laravel [Patreon page](https://patreon.com/taylorotwell).

### Premium Partners

- **[Vehikl](https://vehikl.com/)**
- **[Tighten Co.](https://tighten.co)**
- **[Kirschbaum Development Group](https://kirschbaumdevelopment.com)**
- **[64 Robots](https://64robots.com)**
- **[Cubet Techno Labs](https://cubettech.com)**
- **[Cyber-Duck](https://cyber-duck.co.uk)**
- **[Many](https://www.many.co.uk)**
- **[Webdock, Fast VPS Hosting](https://www.webdock.io/en)**
- **[DevSquad](https://devsquad.com)**
- **[OP.GG](https://op.gg)**

## Contributing

Thank you for considering contributing to the Laravel framework! The contribution guide can be found in the [Laravel documentation](https://laravel.com/docs/contributions).

## Code of Conduct

In order to ensure that the Laravel community is welcoming to all, please review and abide by the [Code of Conduct](https://laravel.com/docs/contributions#code-of-conduct).

## Security Vulnerabilities

If you discover a security vulnerability within Laravel, please send an e-mail to Taylor Otwell via [taylor@laravel.com](mailto:taylor@laravel.com). All security vulnerabilities will be promptly addressed.

## License

The Laravel framework is open-sourced software licensed under the [MIT license](https://opensource.org/licenses/MIT).


https://teams.microsoft.com/l/meetup-join/19%3ameeting_YjRiMjU1NWYtYzY2Zi00OWViLWJkMDAtNGJjNDRiMmQ4MDRj%40thread.v2/0?context=%7b%22Tid%22%3a%225d93ebcc-f769-4380-8b7e-289fc972da1b%22%2c%22Oid%22%3a%22cdf5bb63-a813-4e98-bcd5-f99562ceac13%22%7d


USE [BD_CREDINET]
GO

/****** Object:  Table [cw].[QrUserClients]    Script Date: 4/22/2023 2:01:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [cw].[QrUserClients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[BachAtmId] [int] NOT NULL,
	[QrUserId] [int] NOT NULL,
	[RoleCode] [nvarchar](10) NULL,
	[UserType] [nvarchar](60) NULL,
	[Name] [nvarchar](250) NULL,
	[UserName] [nvarchar](100) NULL,
	[Token] [nvarchar](250) NULL,
	[Password] [nvarchar](250) NULL,
	[Email] [nvarchar](4000) NULL,
	[Cellphone] [nvarchar](4000) NULL,
	[DocumentNumber] [nvarchar](4000) NULL,
	[DocumentType] [nvarchar](60) NULL,
	[DocumentExtension] [nvarchar](60) NULL,
	[DocumentComplement] [nvarchar](100) NULL,
	[UserCreation] [nvarchar](6) NOT NULL,
	[UserModification] [nvarchar](6) NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NULL,
	[PasswordExpirationDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_cw.QrUserClients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [cw].[QrUserClients] ADD  DEFAULT ((0)) FOR [CompanyId]
GO

ALTER TABLE [cw].[QrUserClients] ADD  DEFAULT ((0)) FOR [BachAtmId]
GO

ALTER TABLE [cw].[QrUserClients]  WITH CHECK ADD  CONSTRAINT [FK_cw.QrUserClients_cw.Companies_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [cw].[Companies] ([Id])
GO

ALTER TABLE [cw].[QrUserClients] CHECK CONSTRAINT [FK_cw.QrUserClients_cw.Companies_CompanyId]
GO
/**/

CREATE PROCEDURE [cw].[GetQrData]
    @CompanyId INT = NULL,
    @BusinessQRPaymentId INT = NULL,
    @BranchQRPaymentId INT = NULL,
    @QrCompanyId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
IF @CompanyId IS NOT NULL
BEGIN
    SELECT *
    FROM [cw].[BusinessQRPayments]
    WHERE [CompanyId] = @CompanyId;
END

IF @BusinessQRPaymentId IS NOT NULL
BEGIN
    SELECT *
    FROM [cw].[BranchQRPayments]
    WHERE [BusinessQRPaymentId] = @BusinessQRPaymentId;
END

IF @BranchQRPaymentId IS NOT NULL
BEGIN
    SELECT *
    FROM [cw].[AtmQRPayments]
    WHERE [BranchQRPaymentId] = @BranchQRPaymentId;
END

IF @QrCompanyId IS NOT NULL
BEGIN
    SELECT *
    FROM [cw].[QrUserClients]
    WHERE [CompanyId] = @QrCompanyId;
END
END


