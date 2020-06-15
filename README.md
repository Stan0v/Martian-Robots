# Martian-Robots
Solution
Data transfering between planets is long and expensive procedure. So at first place was performance, data compression, errors control and ability to performance tuning.
That's why I've choosen gRPC as main technology for data transfering between client and server.
Also gRPC commands are backward compatible and can be easily extended. As about task specification commands execute synchronously but can be easily turned to asynchronous mode that better suites our scenario

List of assumptions
1. One point of grid could contain any number of robots
2. Robot could move more than one point at once
2. There could be more than one grid on the surface (and code is ready to support it already)
3. Later with project evolving new robots and grid types would be added (the basis for that is already in code)

For such type of the project it must have detailed log of all the operations and command to read log. This is not realized in code yet btw
Also we need database to store information about grids and robots as this is expensive equipement
Unit tests have been made in basic form and must be expanded
Client application has not been developed
Also refactoring could be made already in some places due to fast code development

Timings:
1. Coordination of customer requirements and technical specifications - 1 day
2. Plan, design, architecture - 1 day
4. Customer communication - 4 hours
5. Programming, testing, debugging - 2 days
6. Customer demonstration and review - 1 day
7. Last fixes and deploy - 1 day
Total 6.5 days with a margin
