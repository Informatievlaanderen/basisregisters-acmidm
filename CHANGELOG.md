## [4.0.1](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v4.0.0...v4.0.1) (2025-04-08)


### Bug Fixes

* nuget dependencies ([cb1cf87](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/cb1cf87eaaf04ec6614792e662dc28bff4c34876))

# [4.0.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v3.3.0...v4.0.0) (2025-04-08)


### Code Refactoring

* use renovate and nuget + update pipeline ([686c36d](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/686c36d1eebeb7aeee0c5278d52a945715760c9a))


### BREAKING CHANGES

* update to dotnet 9

# [3.3.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v3.2.0...v3.3.0) (2024-12-24)


### Features

* add cache settings ([28b9810](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/28b9810a27d1754e8a7f878d26a86ab080fb1526))

# [3.2.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v3.1.0...v3.2.0) (2024-11-04)


### Features

* upgrade niscode package set validfrom ([5f5be3d](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/5f5be3d44b32565511377af721fd1be957639c06))

# [3.1.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v3.0.1...v3.1.0) (2024-11-04)


### Features

* upgrade niscode package ([a43e0bd](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/a43e0bd42930c7d1378de04df968b149ec5253f8))

## [3.0.1](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v3.0.0...v3.0.1) (2024-07-29)


### Bug Fixes

* bump after breaking change hardcoded niscode list ([ef31a1d](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/ef31a1d416b7278e3cc9821496ff3b7e0ab03958))

# [3.0.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v2.1.0...v3.0.0) (2024-07-24)


### Features

* add ovocode blacklist ([0973361](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/0973361a98ef462c8a047c6b2b6ddf804a26a563))


### BREAKING CHANGES

* extension methods registering all policies split up by groups

# [2.1.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v2.0.0...v2.1.0) (2024-05-07)


### Features

* add FindOrgCodeClaim extension + rename IOvoCodeWhiteList to IOrganisationWhiteList ([90ef8b9](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/90ef8b9a7d522c6afa1597a68c54af531c8d6bb2))

# [2.0.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.5.0...v2.0.0) (2024-03-13)


### Features

* move to dotnet 8.0.2 ([d2381eb](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/d2381ebeb3435e57317fcfb416345107945bb6a6))


### BREAKING CHANGES

* move to dotnet 8.0.2

# [1.5.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.4.1...v1.5.0) (2024-01-08)


### Features

* add HasScope and IsInterneBijwerker extensions ([408f2e5](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/408f2e50c9e84078cf5db89d1d7d935ba7d0706e))

## [1.4.1](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.4.0...v1.4.1) (2023-11-30)


### Bug Fixes

* dependencies ([2bec68b](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/2bec68bba58012674ab00b94a6c60719f671e3d7))

# [1.4.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.3.6...v1.4.0) (2023-04-21)


### Features

* renamed packages using Auth and added niscode authorization ([b195677](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/b195677799225c7d2a2293df75df1838b5ff9c1a))

## [1.3.6](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.3.5...v1.3.6) (2023-03-31)


### Bug Fixes

* add road-registry scopes ([614746c](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/614746cacc2a96adbf6cdb2402357e7bfe56c29e))

## [1.3.5](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.3.4...v1.3.5) (2023-01-16)


### Bug Fixes

* lock dependencies in paket.template ([ec29a29](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/ec29a2946e0daebbba8533a8b7824444d8321792))
* lock dependencies in paket.template ([b97bb60](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/b97bb602a0370e36420505784964d20b4fa29a5a))

## [1.3.4](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.3.3...v1.3.4) (2023-01-13)


### Bug Fixes

* disable NisCodeService to enable local debugging ([6ff0d04](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/6ff0d04be5c47be1273bd793a890d478e9e80606))

## [1.3.3](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.3.2...v1.3.3) (2023-01-10)


### Bug Fixes

* add coverage to build.yml ([e00454a](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/e00454a3ad932026d20e85cb7377ea9164a00d11))
* dummy commit ([c767964](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/c767964aa059cb8804ecfdde2355ffa408c7483d))

## [1.3.2](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.3.1...v1.3.2) (2023-01-10)


### Bug Fixes

* check niscode claim presence before fetching from niscode-service ([36a87a2](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/36a87a2b5a5452e54877e2ccf8b57171f0751cf7))

## [1.3.1](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.3.0...v1.3.1) (2023-01-10)


### Bug Fixes

* integrate NisCodeService ([11c6943](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/11c6943b29c57ade82c6d0bf01b6ffd6e306b567))

# [1.3.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.2.0...v1.3.0) (2022-12-16)


### Features

* add building policies ([43b6437](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/43b6437f8ba33b6dc437faa0a40f9bb8f53185f8))

# [1.2.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.1.2...v1.2.0) (2022-12-15)


### Features

* add policies ([2d8fec8](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/2d8fec885134cf6570818625dfa5428b597fcf09))

## [1.1.2](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.1.1...v1.1.2) (2022-12-15)


### Bug Fixes

* remove dependency on Microsoft.IdentityModel.Tokens ([525dabf](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/525dabf0992394e9aaf2841f2d4f2e49d60e648e))

## [1.1.1](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.1.0...v1.1.1) (2022-12-15)


### Bug Fixes

* don't initialize AcmIdmPolicyOptions.AllowedScopeValues ([13bf4d4](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/13bf4d4d7e6e3e2ea66176e870f77c796f3c1866))

# [1.1.0](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.0.3...v1.1.0) (2022-12-14)


### Features

* expose AcmIdmPolicyOptions + rename AcmIdmAuthorizationHandler ([2cb7617](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/2cb7617e145b4b5b2f2f5b8a108c4ebb064af0c5))

## [1.0.3](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.0.2...v1.0.3) (2022-12-14)


### Bug Fixes

* dummy commit ([99201c2](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/99201c28c05c90cc68c533a9a41657f2d1953997))
* nuget ([b780530](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/b780530fc5967aca4a87c14896c9e072746013b6))
* use GITHUB_TOKEN ([f6c97b1](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/f6c97b10fb11e211bb5f2f20a77e11e47ad34ce8))

## [1.0.2](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.0.1...v1.0.2) (2022-12-14)


### Bug Fixes

* set token to github token ([4f4e713](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/4f4e7138deaad893b12400ff490130bc9dacf6bc))

## [1.0.1](https://github.com/informatievlaanderen/basisregisters-acmidm/compare/v1.0.0...v1.0.1) (2022-12-13)


### Bug Fixes

* make Program static ([bb6fea9](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/bb6fea9c9b60ffc6ce58307ce94c26a6b2e88b11))

# 1.0.0 (2022-12-13)


### Bug Fixes

* add npm files ([002e50b](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/002e50b477a66969155996b2d73224b10e462e3a))
* build.fsx ([acc5bb6](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/acc5bb6fd161df4d840869df2180d0f8887940dd))
* clean up ([0044fc5](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/0044fc511a0552dbff213bd818fede2aea3febcb))
* cleanup ([3d30905](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/3d3090597abb91d0342ba97719102a1ee2c1975f))
* don't make Program static ([a0c424f](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/a0c424f4ff2b048ab20e316974f3c29eddf021b6))
* fix sonar key ([88ff394](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/88ff3943f46b32b15cb289124ea89efb28b33591))
* include Be.Vlaanderen.Basisregisters.Build.Pipeline ([ed8da3f](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/ed8da3f59b1e3dadea9330df7d32861d2aa6bb00))
* make build.sh executable ([2ec33d8](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/2ec33d88702884af62fb88d6d6faf63fca6be185))
* make classes static ([3588352](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/3588352f1ce9414a9ac82e39a3f96605b90480be))
* no release on push ([f669463](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/f66946389fbd58aa891da4efb1c15ecf3046a3ed))
* re-enable test ([d12ea97](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/d12ea97249097c56a435b3d039a8bf56352dfea7))
* release is release ([fb7e6bd](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/fb7e6bd168a14f97a40b4f0c502e524debe7d171))
* remove test from build ([4ff66c0](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/4ff66c070c93e9fc9c2bf1edb7112c95affb07dc))
* remove WaitForPort ([0ec6acd](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/0ec6acdccf04b4976f91c883bc0fc96ceb218c5f))
* update build.fsx ([9ee6c92](https://github.com/informatievlaanderen/basisregisters-acmidm/commit/9ee6c921eb713720ee1e0da517f83fa94a231792))
