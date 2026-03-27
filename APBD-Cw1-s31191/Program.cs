using APBD_Cw1_s31191;

var userRepository = new UserRepository();
var hardwareRepository = new HardwareRepository();
var leaseRepository = new LeaseRepository();

var userService = new UserService(userRepository);
var hardwareService = new HardwareService(hardwareRepository);
var leaseService = new LeaseService(hardwareService, userService, leaseRepository);

var app = new App(userService, hardwareService, leaseService);
app.Run();
